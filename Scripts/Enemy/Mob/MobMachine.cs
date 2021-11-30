using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobMachine : MonoBehaviour, IDamagable
{
    // Components
    NavMeshAgent _agent;
    Animator _animator;

    // Transform of the character's head for vision detection
    [SerializeField] Transform _eyes;

    // Player or target from Searching state
    Transform _target;

    // Attacking
    [SerializeField] Collider _weaponHitbox;
    bool _attacking;

    [SerializeField] float _sightRange;
    [SerializeField] float _chaseRange;
    [SerializeField] float _attackRange;
    [SerializeField] int _numberOfAttacks;

    // animation hashes
    int _attackCountHash;
    int _hurtHash;
    int _deadHash;

    MobBase _currentState;
    MobFactory _states;

    // general references
    public NavMeshAgent Agent { get { return _agent; }}
    public MobBase CurrentState { get { return _currentState; } set { _currentState = value; }}
    public Transform Target { get { return _target; } set { _target = value; }}
    public Transform Eyes { get { return _eyes; }}
    public bool IsDead { get { return _isDead; }}

    // animation references
    public Animator Animator { get { return _animator; }}
    public int AttackCountHash { get { return _attackCountHash; }}
    public int DeadHash { get { return _deadHash; }}

    // state-specific references
    public bool Attacking { get { return _attacking; } set { _attacking = value; }}
    public int LayerMask { get { return 1 << 10; }}
    public int NumberOfAttacks { get { return _numberOfAttacks; }}
    public float SightRange { get { return _sightRange; }}
    public float ChaseRange { get { return _chaseRange; }}
    public float AttackRange { get { return _attackRange; }}


    // health and damage functions
    [Header("Health")]
    [SerializeField] int _maxHealth;
    [SerializeField] int _hurtThreshold;

    bool _isDead = false, _isRecovering = false;
    int _currentHealth;
    WaitForSeconds delay = new WaitForSeconds(0.1f);

    // Methods
    void Awake()
    {
        // Find components
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        // Initialize State Behaviour
        _states = new MobFactory(this);
        _currentState = _states.Searching();
        _currentState.EnterState(); 

        // animator hashes
        _attackCountHash = Animator.StringToHash("attackNumber");
        _hurtHash = Animator.StringToHash("hurt");
        _deadHash = Animator.StringToHash("dead");

        // health
        _currentHealth = _maxHealth;
        StartCoroutine(IHealthChecker());
    }

    void Update()
    {
        _currentState.UpdateStates();
    }

    // Use-specific methods
    void AttackReset() {
        // called from animation to end attack cycle
        _attacking = false;
        Animator.SetInteger(_attackCountHash, 0);
    }

    public void ToggleWeaponHitbox() {
        // called from animation to toggle damage on and off
        _weaponHitbox.enabled = !_weaponHitbox.enabled;
    }

    // Health functions
    public void Damage(int damage) {
        if (!_isRecovering) {
            _currentHealth -= damage;

            if (_currentHealth > 0 && damage > _hurtThreshold) {
                Animator.SetTrigger(_hurtHash);
            }
        }
    }

    public int MaxHealth() {
        return _maxHealth;
    }

    IEnumerator IHealthChecker() {
        while (!_isDead) {
            if (_currentHealth <= 0) 
            {
                _isDead = true; 
            }
               
            yield return delay;
        }
    }
}
