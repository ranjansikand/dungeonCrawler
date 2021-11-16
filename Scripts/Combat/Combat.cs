using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour, IDamagable
{
    [SerializeField] Collider weaponHitbox;
    [SerializeField] CharacterController controller;

    [SerializeField] int explosiondmg = 1;
    [SerializeField] Transform _tipOfWeapon;
    [SerializeField] float explosionRadius = 1.5f;

    [SerializeField] int maxHealth = 10;
    int health;
    PlayerStateMachine player;
    bool invulnerable = false;

    void Awake()
    {
        player = GetComponent<PlayerStateMachine>();
        health = maxHealth;

        StartCoroutine("CheckForDeath");
    }

    IEnumerator CheckForDeath()
    {
        while (!player.IsDead)
        {
            if (health <= 0) Die();
            yield return new WaitForSeconds(.1f);
        }
    }

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

    public void ToggleInvulnerable()
    {
        // call from the dodge to disable and enable hitbox
        invulnerable = !invulnerable;
    }

    public void Damage(int damage)
    {
        if (!invulnerable) health -= damage;

        Debug.Log(health);
    }

    void Die()
    {
        StopCoroutine("CheckForDeath");
        player.IsDead = true;
        player.Animator.SetTrigger("Die");
    }
}
