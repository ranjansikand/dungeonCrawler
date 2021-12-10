using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtk : MonoBehaviour
{
    Collider[] results = new Collider[10];

    [SerializeField] GameObject _particleEffect;
    [SerializeField] Transform _tipOfSword;

    public void GroundSmash()
    {
        if (_particleEffect != null) {
            Instantiate(_particleEffect, _tipOfSword.position, Quaternion.identity);
        }
        
        int hits = Physics.OverlapSphereNonAlloc(_tipOfSword.position, .25f, results);
        if (hits > 0) {
            for (int i = 0; i < hits; i++) 
            {
                var hitTarget = results[i].gameObject.GetComponent<IDamagable>();
                hitTarget?.Damage(1);
            }
        }
    }
}
