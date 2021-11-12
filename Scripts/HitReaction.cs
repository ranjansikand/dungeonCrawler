using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Item drop;
    int hits = 0;
    bool dead;

    void Update()
    {
        if (hits == 2 && !dead)
        {
            _animator.SetTrigger("dead");
            dead = true;
            Invoke("Death", 1f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            _animator.SetTrigger("hurt");
            hits++;
        }
    }

    void Death()
    {
        Instantiate(drop.prefab, transform.position + drop.basePosition, transform.rotation);
        Destroy(gameObject);
    }
}
