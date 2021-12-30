using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] int dmg = 1;

    void OnTriggerEnter(Collider other)
    {
        var hitTarget = other.GetComponent<IDamagable>();
        hitTarget?.Damage(dmg);
    }
}