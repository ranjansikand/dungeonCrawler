using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Behavior")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float maxSightRange;

    [SerializeField] float minPatrol, maxPatrol, routeTime;
    [SerializeField] bool pinToStartingLocation;
    [SerializeField] int groundLayer;

    private Collider target;
    const int layerMask = 1 << 10;
    static Collider[] targetsBuffer = new Collider[100];
    public Transform player;
    Vector3 startingLocation;


    [Header("Health")]
    [SerializeField] int health;


    public virtual void Start()
    {
        if (pinToStartingLocation) startingLocation = transform.position;

        StartCoroutine("Patrol");
    }


    public virtual void Update()
    {
        AcquireTarget();

        if (player != null) {
            StopCoroutine("Patrol");
        }
    }


    bool AcquireTarget () {
        Vector3 a = transform.localPosition;
        Vector3 b = a;
        b.y += 2f;
        int hits = Physics.OverlapCapsuleNonAlloc(
            a, b, maxSightRange, targetsBuffer, layerMask
        );
        if (hits > 0) {
            for (int i = 0; i < hits; i++)
            {
                target = targetsBuffer[i].GetComponent<Collider>();
                player = target.gameObject.transform;
            }
            Debug.Assert(target != null, "Targeted non-enemy!", targetsBuffer[0]);
        }
        target = null;
        return false;
    }

    void OnTriggerEnter(Collider other) { if (other.gameObject.layer == 11) Hurt(); }

    public void Hurt() { health--; }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!pinToStartingLocation) {
                startingLocation = transform.position;
            }

            Vector3 destination = new Vector3(
                    startingLocation.x + Random.Range(-1, 2) * Random.Range(minPatrol, maxPatrol), 
                    startingLocation.y, 
                    startingLocation.x + Random.Range(-1, 2) * Random.Range(minPatrol, maxPatrol)
                );

            agent.SetDestination(destination);

            yield return new WaitForSeconds(routeTime);
        }
    }
}
