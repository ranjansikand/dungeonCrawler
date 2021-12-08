using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
    // singleton for Inputs
    #region 
    public static CameraManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple inventories found!");
            return;
        }

        instance = this;
    }
    #endregion

    // public variables
    [Header("Transforms")]
    public Transform player;

    [Header("Cameras")]
    public GameObject orbitCamera;
    public GameObject lockOnCamera;

    [Header("Reticle")]
    public TrackTarget reticle;
    
    [Header("Cinemachine variables")]
    public CinemachineTargetGroup targetGroup;
    public float weight = 0.4f, radius = 2f;

    // private variables
    Transform target;
    IDamagable targetHit;
    PlayerStateMachine playerStateMachine;
    int layerMask = 1 << 15;
    WaitForSeconds delay = new WaitForSeconds(0.1f);

    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStateMachine = player.gameObject.GetComponent<PlayerStateMachine>();
    }

    IEnumerator ICheckTarget() {
        while (true) {
            if (targetHit == null || targetHit?.CurrentHealth() <= 0) RevertToOrbit();
            yield return delay;
        }
    }

    public void OnLockOn(bool lockedOn)
    {
        // handles locking and unlocking
        if (!lockedOn) {
            SwitchToLockOn();
        }
        else {
            RevertToOrbit();
        }

        playerStateMachine.Target = target;
    }

    void SwitchToLockOn()
    {
        Collider[] results = new Collider[25];
        int hits = Physics.OverlapSphereNonAlloc(player.position, 10f, results, layerMask);

        if (hits > 0){
            float magnitude = 100;
            for (int i = 0; i < hits; i++) {
                // check for closest enemy to the player
                Vector3 thisPos = player.InverseTransformPoint(results[i].transform.position);
                if (thisPos.magnitude < magnitude) {
                    target = results[i].transform;
                    magnitude = thisPos.magnitude;
                }
            }
            
            // to check if target is dead
            targetHit = target.GetComponent<IDamagable>();

            // update lock Target Group
            lockOnCamera.GetComponent<CinemachineVirtualCamera>().m_LookAt = target;

            // update cameras on success
            lockOnCamera.SetActive(true);
            orbitCamera.SetActive(false);
            StartCoroutine(ICheckTarget());

            // update reticle
            reticle.gameObject.SetActive(true);
            reticle.Target = target;
        }
    }

    void RevertToOrbit()
    {
        StopCoroutine(ICheckTarget());

        orbitCamera.SetActive(true);
        lockOnCamera.SetActive(false);

        target = null;
        playerStateMachine.IsLockedOn = false;

        // update reticle
        reticle.Target = null;
        reticle.gameObject.SetActive(false);
    }
}
