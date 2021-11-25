using System.Collections;
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
    public Transform player;
    public GameObject orbitCamera, lockOnCamera;
    public CinemachineTargetGroup targetGroup;
    public float weight = 0.4f, radius = 2f;

    // private variables
    Transform target;
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
            if (target == null) RevertToOrbit();
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
        RaycastHit hit;
        if (Physics.SphereCast(player.position, 5f, transform.forward, out hit, 24f, layerMask))
        {
            target = hit.collider.transform;
            Debug.Log("Found target: " + target.name);

            // update lock Target Group
            CinemachineTargetGroup.Target updatedTarget; 
            updatedTarget.target = target;
            updatedTarget.weight = weight;
            updatedTarget.radius = radius;

            targetGroup.m_Targets.SetValue(updatedTarget, 1);

            // update cameras on success
            lockOnCamera.SetActive(true);
            orbitCamera.SetActive(false);
            StartCoroutine(ICheckTarget());
        }
    }

    void RevertToOrbit()
    {
        StopCoroutine(ICheckTarget());

        orbitCamera.SetActive(true);
        lockOnCamera.SetActive(false);

        target = null;
    }
}
