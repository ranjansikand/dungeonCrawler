using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour, IDamagable
{
    // [SerializeField] Image _healthbar;
    [SerializeField] Healthbar playerHealthBar;

    [Header("General combat stats")]
    [SerializeField] GameObject _weapon;
    [SerializeField] CharacterController _controller;
    
    Collider _weaponHitbox;
    HitboxProcess _hitboxStats;
    ParticleSystem _weaponSlash;


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
    [SerializeField] Shader _hurtShader;

    Renderer[] _renderers;


    PlayerStateMachine player;
    bool _invulnerable = false;

    void Awake()
    {
        player = GetComponent<PlayerStateMachine>();
        _health = _maxHealth;

        _weaponHitbox = _weapon.GetComponent<Collider>();
        _hitboxStats = _weapon.GetComponent<HitboxProcess>();
        _weaponSlash = _weapon.GetComponentInChildren<ParticleSystem>();

        _renderers = GetComponentsInChildren<Renderer>();

        PlayerStateMachine.onBlockStarted += OnBlockStarted;
        PlayerStateMachine.onBlockEnded += OnBlockEnded;

        // UpdateHealthbar();
        playerHealthBar.InitializeHealth(_maxHealth);
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
        _invulnerable = true;
    }
    void OnBlockEnded() {
        _invulnerable = false;
    }

    public void ActivateHitbox()
    {
        // Called at the beginning of player's attack's damage period
        _weaponHitbox.enabled = true;

        // enable slash effect
        _weaponSlash.Play();
    }

    public void DeactivateHitbox()
    {
        // Called at the end of attack damage period
        _weaponHitbox.enabled = false;

        // disable slash effect
        _weaponSlash.Stop();
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

    public void Damage(int damage)
    {
        if (_invulnerable) return;
        
        _health -= damage;

        // UpdateHealthbar();
        playerHealthBar.TakeDamage(damage);
        HurtShaderEffect();
        Invoke("HurtShaderEffect", 0.25f);

        Debug.Log(_health);
    }

    // void UpdateHealthbar()
    // {
    //     _healthbar.fillAmount = 1.0f  * _health / _maxHealth;
    // }

    public int CurrentHealth()
    {
        // required for IDamagable
        return _health;
    }

    void Die()
    {
        StopCoroutine("CheckForDeath");
        player.IsDead = true;
        player.Animator.SetTrigger("Die");
    }

    void HurtShaderEffect()
    {
        Shader newShader;

        if (_renderers[0].material.shader == _standardShader) newShader = _hurtShader;
        else newShader = _standardShader;

        for (int i = 0; i < _renderers.Length; i++) {
            _renderers[i].material.shader = newShader;
        }
    }
}
