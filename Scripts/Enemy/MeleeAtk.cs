using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtk : MonoBehaviour
{
    private MobMachine _thisMachine;

    Collider[] _damageable = new Collider[10];
    Collider[] _enemies = new Collider[10];

    [SerializeField] GameObject _particleEffect;
    [SerializeField] Transform _tipOfSword;

    void Awake()
    {
        _thisMachine = GetComponent<MobMachine>();
    }

    public void GroundSmash()
    {
        if (_particleEffect != null) {
            Instantiate(_particleEffect, _tipOfSword.position, Quaternion.identity);
        }
        
        int hits = Physics.OverlapSphereNonAlloc(_tipOfSword.position, .25f, _damageable);
        if (hits > 0) {
            for (int i = 0; i < hits; i++) 
            {
                var hitTarget = _damageable[i].gameObject.GetComponent<IDamagable>();
                hitTarget?.Damage(1);
            }
        }
    }

    public void CallForBackup()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, 12f, _enemies, 1 << 15);

        if (hits > 0) {
            for (int i = 0; i < hits; i++) {
                var machine = _enemies[i].gameObject.GetComponent<IDetection>();
                machine?.AssignTarget(_thisMachine.Target);
            }
        }
    }
}
