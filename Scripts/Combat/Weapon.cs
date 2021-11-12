using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Collider weaponHitbox;

    public void ToggleHitbox()
    {
        weaponHitbox.enabled = !weaponHitbox.enabled;
    }
}
