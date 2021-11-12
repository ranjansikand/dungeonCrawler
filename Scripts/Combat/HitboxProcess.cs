using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxProcess : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var hitTarget = other.GetComponent<IDamagable>();
        hitTarget?.Damage(3);
    }
}
