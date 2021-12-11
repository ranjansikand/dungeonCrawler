using UnityEngine;
using UnityEngine.AI;

public class MobMachine : MonoBehaviour, IDamagable, IDetection
{
    // Components
    NavMeshAgent _agent;
    Animator _animator;
    Collider _bodyCollider;

    // Transform of the character's head for vision detection
    [SerializeField] Transform _eyes;

    // Player or target from Searching state
    Transform _target;

    // Attacking
    [SerializeField] Collider _weaponHitbox;
    bool _attacking;

    [SerializeField] float _sightRange;
    [SerializeField] float _chaseRange;
    [SerializeField] float _circleRange;
    [SerializeField] float _attackRange;
    [SerializeField] int _numberOfAttacks;

    // Detection feedbacks
    [Header("Sprites")]
    [SerializeField] StatusSprite _status;
    [SerializeField] Sprite _confused;
    [SerializeField] Sprite _alarmed;

    // animation hashes
    int _attackCountHash;
    int _hurtHash;
    int _deadHash;
    int _circlingHash;

    MobBase _currentState;
    MobFactory _states;

    // general references
    public NavMeshAgent Agent { get { return _agent; }}
    public MobBase CurrentState { get { return _currentState; } set { _currentState = value; }}
    public Transform Target { get { return _target; } set { _target = value; }}
    public bool IsDead { get { return _isDead; }}

    // Sprites
    public StatusSprite Status { get { return _status; }}
    public Sprite Confused { get { return _confused; }}
    public Sprite Alarmed { get { return _alarmed; }}

    // animation references
    public Animator Animator { get { return _animator; }}
    public int AttackCountHash { get { return _attackCountHash; }}
    public int DeadHash { get { return _deadHash; }}
    public int CirclingHash { get { return _circlingHash; }}

    // state-specific references
    public bool Attacking { get { return _attacking; } set { _attacking = value; }}
    public int LayerMask { get { return 1 << 10; }}
    public int NumberOfAttacks { get { return _numberOfAttacks; }}
    public float SightRange { get { return _sightRange; }}
    public float ChaseRange { get { return _chaseRange; }}
    public float CircleRange { get { return _circleRange; }}
    public float AttackRange { get { return _attackRange; }}


    // health and damage functions
    [Header("Health")]
    [SerializeField] int _maxHealth;
    [SerializeField] int _hurtThreshold;

    [SerializeField] GameObject _bloodSplatter;

    bool _isDead = false, _isRecovering = false;
    int _currentHealth;
    WaitForSeconds delay = new WaitForSeconds(0.1f);

    // Methods
    void Awake()
    {
        // Find components
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _bodyCollider = GetComponent<Collider>();

        // Initialize State Behaviour
        _states = new MobFactory(this);
        _currentState = _states.Searching();
        _currentState.EnterState(); 

        // animator hashes
        _attackCountHash = Animator.StringToHash("attackNumber");
        _hurtHash = Animator.StringToHash("hurt");
        _deadHash = Animator.StringToHash("dead");
        _circlingHash = Animator.StringToHash("circling");

        // health
        _currentHealth = _maxHealth;
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
            _isRecovering = true;
            _currentHealth -= damage;
            if (_bloodSplatter != null) Instantiate(_bloodSplatter, transform.position, Quaternion.identity);

            if (_currentHealth > 0) {
                if (damage > _hurtThreshold) Animator.SetTrigger(_hurtHash);
            } else {
                _isDead = true; 
                _bodyCollider.enabled = false;
            }
            Invoke("EndRecovery", 0.25f);
        }
    }

    void EndRecovery()
    {
        // called by damage coroutine
        _isRecovering = false;
    }

    public int CurrentHealth() {
        return _currentHealth;
    }

    public void AssignTarget(Transform target) {
        if (_target == null) {
            _target = target;
        }
    }
}
