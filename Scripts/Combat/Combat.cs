using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour, IDamagable
{
    [Header("General combat stats")]
    [SerializeField] GameObject _weapon;
    [SerializeField] int _damage = 1;
    [SerializeField] CharacterController _controller;
    
    Collider _weaponHitbox;
    HitboxProcess _hitboxStats;


    [Header("Explosions")]
    [SerializeField] int explosiondmg = 1;
    [SerializeField] Transform _tipOfWeapon;
    [SerializeField] float explosionRadius = 1.5f;


    [Header("Health")]
    [SerializeField] int _maxHealth = 10;
    int _health;


    [Header("Blocking")]
    [SerializeField] GameObject _parentObj;
    [SerializeField] Shader _standardShader;
    [SerializeField] Shader _blockingShader;

    Renderer[] _renderers;


    PlayerStateMachine player;
    bool _invulnerable = false;

    void Awake()
    {
        player = GetComponent<PlayerStateMachine>();
        _health = _maxHealth;

        _weaponHitbox = _weapon.GetComponent<Collider>();
        _hitboxStats = _weapon.GetComponent<HitboxProcess>();

        _renderers = GetComponentsInChildren<Renderer>();

        PlayerStateMachine.onBlockStarted += OnBlockStarted;
        PlayerStateMachine.onBlockEnded += OnBlockEnded;

        _hitboxStats.SetDamage(_damage);
        StartCoroutine(CheckForDeath());
    }

    IEnumerator CheckForDeath()
    {
        while (!player.IsDead)
        {
            if (_health <= 0) Die();
            yield return new WaitForSeconds(.1f);
        }
    }

    void OnBlockStarted() {
        // instantiate effect
        for (int i = 0; i < _renderers.Length; i++) {
            _renderers[i].material.shader = _blockingShader;
        }
        // negate damaging
        _invulnerable = true;
        _hitboxStats.SetDamage(0);
    }
    void OnBlockEnded() {
        // end effect
        for (int i = 0; i < _renderers.Length; i++) {
            _renderers[i].material.shader = _standardShader;
        }
        // re-enable damage
        _invulnerable = false;
        _hitboxStats.SetDamage(_damage);
    }

    public void ToggleHitbox()
    {
        // call from attack animations to enable impacts
        _weaponHitbox.enabled = !_weaponHitbox.enabled;
    }

    public void ToggleCharacterCollisions()
    {
        _controller.detectCollisions = !_controller.detectCollisions;
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
        _invulnerable = !_invulnerable;
    }

    public void Damage(int damage)
    {
        if (!_invulnerable) _health -= damage;

        Debug.Log(_health);
    }

    public int MaxHealth()
    {
        // required for IDamagable
        return _maxHealth;
    }

    void Die()
    {
        StopCoroutine("CheckForDeath");
        player.IsDead = true;
        player.Animator.SetTrigger("Die");
    }
}
