using UnityEngine;

public class HitboxProcess : MonoBehaviour
{
    [SerializeField] int dmg = 1;

    public void SetDamage(int damage)
    {
        dmg = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        var hitTarget = other.GetComponent<IDamagable>();
        hitTarget?.Damage(dmg);
    }
}
