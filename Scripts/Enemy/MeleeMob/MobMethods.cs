using UnityEngine;

public class MobMethods : MonoBehaviour
{
    MobStateMachine _thisStateMachine;

    [SerializeField] Collider weaponHitbox;

    void Awake()
    {
        _thisStateMachine = GetComponent<MobStateMachine>();

        weaponHitbox.enabled = false;
    }

    void StartAttack()
    {
        weaponHitbox.enabled = true;
    }

    void AttackReset()
    {
        weaponHitbox.enabled = false;
        _thisStateMachine.IsAttacking = false;
    }
}
