using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour, IDamagable
{
    [Header("General combat stats")]
    [SerializeField] GameObject weapon;
    [SerializeField] int damage = 1;
    [SerializeField] CharacterController controller;
    
    Collider weaponHitbox;
    HitboxProcess hitboxStats;


    [Header("Explosions")]
    [SerializeField] int explosiondmg = 1;
    [SerializeField] Transform _tipOfWeapon;
    [SerializeField] float explosionRadius = 1.5f;


    [Header("Health")]
    [SerializeField] int maxHealth = 10;
    int health;


    PlayerStateMachine player;
    bool invulnerable = false;

    void Awake()
    {
        player = GetComponent<PlayerStateMachine>();
        health = maxHealth;

        weaponHitbox = weapon.GetComponent<Collider>();
        hitboxStats = weapon.GetComponent<HitboxProcess>();

        PlayerStateMachine.onBlockStarted += OnBlockStarted;
        PlayerStateMachine.onBlockEnded += OnBlockEnded;

        hitboxStats.SetDamage(damage);
        StartCoroutine(CheckForDeath());
    }

    IEnumerator CheckForDeath()
    {
        while (!player.IsDead)
        {
            if (health <= 0) Die();
            yield return new WaitForSeconds(.1f);
        }
    }

    void OnBlockStarted() {
        // instantiate effect
        invulnerable = true;
        hitboxStats.SetDamage(0);
    }
    void OnBlockEnded() {
        // destroy effect
        invulnerable = false;
        hitboxStats.SetDamage(damage);
    }

    public void ToggleHitbox()
    {
        // call from attack animations to enable impacts
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

    public int MaxHealth()
    {
        // required for IDamagable
        return maxHealth;
    }

    void Die()
    {
        StopCoroutine("CheckForDeath");
        player.IsDead = true;
        player.Animator.SetTrigger("Die");
    }
}
