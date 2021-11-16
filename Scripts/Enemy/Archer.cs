using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Archer : MonoBehaviour, IDamagable
{
    NavMeshAgent agent;
    Animator anim;
    Transform target;

    // search
    Collider[] result;
    int layermask = 1 << 10;

    [SerializeField] GameObject arrow;
    [SerializeField] Transform firePoint;
    [SerializeField] Aim firePointAim;
    GameObject currentArrow;

    int health = 2;
    bool dead = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator>();

        PlayerEscaped();
    }

    void Update()
    {
        if (!dead) {
            if (health <= 0) {
                dead = true;
                anim.SetTrigger("die");
                Invoke(nameof(Death), 2.5f);
            }
        }
    }

    IEnumerator RandomPosition()
    {
        while (!dead)
        {
            agent.SetDestination(transform.position + new Vector3(
                Random.Range(-10f, 10f), 0,
                Random.Range(-10f, 10f)));

            yield return new WaitForSeconds(8f);
        }
    }

    IEnumerator SearchForPlayer()
    {
        while (!dead)
        {
            int hits = Physics.OverlapSphereNonAlloc(transform.position, 10f, result, layermask);
            if (hits > 0) {
                Debug.Log("Detected");
                PlayerDetected(result[0]);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator CheckDistance()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(1f);

            if (target != null){
                if (Vector3.Distance(target.position, transform.position) > 20)
                {
                    PlayerEscaped();
                }
            }
        }
    }

    void PlayerDetected(Collider hit)
    {
        // stop search
        StopCoroutine("RandomPosition");
        StopCoroutine("SearchForPlayer");

        agent.SetDestination(transform.position);

        // save info
        target = hit.gameObject.transform;

        // start attack
        StartCoroutine("CheckDistance");
        anim.SetBool("detected", true);
        Debug.Log("Will attack " + target);
    }

    void PlayerEscaped()
    {
        // reset animation state
        anim.SetBool("detected", false);

        // resume coroutines
        StopCoroutine("CheckDistance");
        StartCoroutine("RandomPosition");
        StartCoroutine("SearchForPlayer");

        // reset variables
        target = null;
    }

    void Death()
    {
        StopCoroutine("CheckHealth");

        // particle effect

        Destroy(gameObject);
    }

    public void GenerateArrow()
    {
        // firePointAim.UpdateTarget(target);
    }

    public void FireArrow()
    {
        currentArrow = Instantiate(arrow, firePoint.position, Quaternion.identity);
        currentArrow.GetComponent<Rigidbody>().AddForce(firePoint.forward * 30f, ForceMode.Impulse);
    }

    public void Damage(int damage)
    {
        health -= damage;
        anim.SetTrigger("hurt");
    }
}
