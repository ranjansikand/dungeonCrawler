using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour, IDamagable
{
    [SerializeField] Animator _animator;
    [SerializeField] Item drop;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float arrowForce;

    [SerializeField] Aim firePointAim;
    [SerializeField] float maxHits = 2;

    GameObject currentArrow;
    Transform player;
    int hits = 0;
    bool dead = false;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        StartCoroutine("CheckForDeath");
    }

    IEnumerator CheckForDeath()
    {
        while (!dead)
        {
            if (hits >= maxHits) Die();
            yield return new WaitForSeconds(.2f);
        }
    }
    
    public void Damage(int damage)
    {
        hits++;
        if (hits != maxHits && !dead) _animator.SetTrigger("Hurt");
    }

    public void GenerateArrow()
    {
        Debug.Log("Here");
        firePointAim.UpdateTarget(player);
    }

    public void FireArrow()
    {
        currentArrow = Instantiate(projectile, firePoint.position, firePoint.rotation);
        currentArrow.GetComponent<Rigidbody>().AddForce(arrowForce * firePoint.forward, ForceMode.Impulse);
    }

    void Die()
    {
        dead = true;
        StopCoroutine("CheckForDeath");
        _animator.SetTrigger("Die");
        Invoke(nameof(Delete), 2.5f);
    }

    void Delete()
    {
        // start particle effect

        Destroy(gameObject);
    }
}
