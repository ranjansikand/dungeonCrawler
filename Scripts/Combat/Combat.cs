using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] Collider weaponHitbox;
    [SerializeField] CharacterController controller;

    [SerializeField] int explosiondmg = 1;
    [SerializeField] Transform _tipOfWeapon;
    [SerializeField] float explosionRadius = 1.5f;

    public void ToggleHitbox()
    {
        weaponHitbox.enabled = !weaponHitbox.enabled;
    }

    public void ToggleCharacterCollisions()
    {
        controller.detectCollisions = !controller.detectCollisions;
    }

    public void GenerateExplosionField()
    {
        Collider[] detected = new Collider[10];

        int hits = Physics.OverlapSphereNonAlloc(_tipOfWeapon.position, explosionRadius, detected);

        if (hits > 0){
            for (int i = 0; i < hits; i++)
            {
                var hitTarget = detected[i].gameObject.GetComponent<IDamagable>();
                hitTarget?.Damage(explosiondmg);
            }
        }
        
    }
}
