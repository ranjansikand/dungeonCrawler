using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float lifetime = 2.5f;
    [SerializeField] int damage = 1;

    float timeElapsed;
    
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= lifetime) Delete();
    }

    void OnTriggerEnter(Collider other)
    {
        var hitTarget = other.GetComponent<IDamagable>();
        hitTarget?.Damage(damage);

        Invoke(nameof(Delete), 0.05f);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
