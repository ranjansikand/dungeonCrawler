using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxProcess : MonoBehaviour
{
    [SerializeField] int dmg = 1;

    void OnTriggerEnter(Collider other)
    {
        var hitTarget = other.GetComponent<IDamagable>();
        hitTarget?.Damage(dmg);
    }
}
