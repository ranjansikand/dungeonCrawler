// State machine for Brute-class enemies
// These enemies carry shields and therefore have logic that 
// encompasses blocking and guard-breaking

using UnityEngine;
using UnityEngine.AI;

public class BruteMachine : MonoBehaviour, IDamagable
{
    // Components
    NavMeshAgent _agent;
    Animator _animator;
    Collider _bodyCollider;

    Transform _target;
    float _distanceToTarget;

    // Blocking state
    bool _isBlocking;
    int _remainingPosture;

    [Header("Ranges")]
    [SerializeField] float _sightRange;
    [SerializeField] float _patrolRange;
    [SerializeField] float _aggroRange;
    [SerializeField] float _attackRange;
    [SerializeField] Vector3[] _waypoints;
    int _waypointIndex = -1;
    Vector3 _pinnedPosition;

    // Attacking state
    [Header("Attacking Parameters")]
    [SerializeField] Collider _weaponHitbox;  // for attacks
    [SerializeField] Collider _shieldHitbox;  // for attack and defense
    [SerializeField] int _numberOfAttacks;
    bool _isAttacking;
    bool _battlecry = false;
    WaitForSeconds _staggerDuration = new WaitForSeconds(2f);


    [Header("Health and Damage")]
    [SerializeField] int _maxHealth; // max damage that can be taken
    [SerializeField] int _maxPosture; // damage taken before staggered
    int _currentHealth;
    bool _guardBroken = false;
    bool _isDead = false;

    [Header("Effects")]
    [SerializeField] ParticleSystem _bloodSplatter;
    [SerializeField] ParticleSystem _hit;
    [SerializeField] float _deathSlowMo = .25f;

    // Animation hashes
    int _blockingHash;
    int _attackIntHash;
    int _lookingHash;
    int _staggerHash;
    int _detectedHash;

    [Header("Sprites")]
    [SerializeField] StatusSprite _status;
    [SerializeField] Sprite _confused;
    [SerializeField] Sprite _alarmed;

    BruteBaseState _currentState;
    BruteStateFactory _states;

    // Getters and Setters
    public NavMeshAgent Agent { get { return _agent; }}
    public Animator Animator { get { return _animator; }}
    public Collider BodyCollider { get { return _bodyCollider; }}

    // Animation hashes
    public int BlockingHash { get { return _blockingHash; }}
    public int AttackIntHash { get { return _attackIntHash; }}
    public int LookingHash { get { return _lookingHash; }}
    public int StaggerHash { get { return _staggerHash; }}
    public int DetectedHash { get { return _detectedHash; }}

    // Sprites
    public StatusSprite Status { get { return _status; }}
    public Sprite Confused { get { return _confused; }}
    public Sprite Alarmed { get { return _alarmed; }}

    // targeting and detection
    public Transform Target { get { return _target; } set { _target = value; }}
    public float DistanceToTarget { get { return _distanceToTarget; } set { _distanceToTarget = value;}}
    public int LayerMask { get { return 1 << 10; }}

    public BruteBaseState CurrentState { get { return _currentState; } set { _currentState = value; }}

    public bool GuardBroken { get { return _guardBroken; } set { _guardBroken = value; }}
    public bool IsDead { get { return _isDead; }}
    public bool IsBlocking { get { return _isBlocking; } set { _isBlocking = value; }}
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; }}
    public bool Battlecry { get { return _battlecry; }}

    public int NumberOfAttacks { get { return _numberOfAttacks; }}
    public int MaxPosture { get { return _maxPosture; }}
    public int CurrentPosture { set { _remainingPosture = value; }}
    public WaitForSeconds StaggerDuration { get { return _staggerDuration; }}
    
    public Vector3 PinnedPosition { get { return _pinnedPosition; }}
    public Vector3[] Waypoints { get { return _waypoints; }}
    public int WaypointIndex { get { return _waypointIndex; } set { _waypointIndex = value; }}
    public float SightRange { get { return _sightRange; }}
    public float PatrolRange { get { return _patrolRange; }}
    public float AggroRange { get { return _aggroRange; }}
    public float AttackRange { get { return _attackRange; }}

    void Awake()
    {
        // Fetch components
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _bodyCollider = GetComponent<Collider>();

        // Assign animation hashes
        _blockingHash = Animator.StringToHash("blocking");
        _attackIntHash = Animator.StringToHash("attack");
        _lookingHash = Animator.StringToHash("looking");
        _staggerHash = Animator.StringToHash("staggered");
        _detectedHash = Animator.StringToHash("detected");

        // One-time setting of variables
        _pinnedPosition = transform.position;
        _currentHealth = _maxHealth;
        _remainingPosture = _maxPosture;

        // Initialize state behavior
        _states = new BruteStateFactory(this);
        _currentState = _states.Searching();
        _currentState.EnterState();
    }

    void Update() 
    {
        _currentState.UpdateStates();
    }

    public void Damage(int damage) 
    {
        _hit.Play();

        if (_isBlocking) {
            _remainingPosture -= damage;
        } else {
            _currentHealth -= damage;
            _bloodSplatter.Stop();
            _bloodSplatter.Play();
        }

        if (_remainingPosture <= 0) {
            Debug.Log("Guard broken!");
            _guardBroken = true;
        }

        if (_currentHealth <= 0) {
            Debug.Log("Dead");
            _isDead = true; 
            HurtEffect();
            Invoke("UndoHurtEffect", _deathSlowMo);
        }

        Debug.Log("Brute current health: " + _currentHealth);
    }

    public int CurrentHealth() {
        return _currentHealth;
    }
    public void ToggleShieldHitbox() {
        // called from animation to toggle damage on and off
        _shieldHitbox.enabled = !_shieldHitbox.enabled;
    }

    public void EnableHitbox()
    {
        _weaponHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        _weaponHitbox.enabled = false;
    }
    public void EndAttack()
    {
        // called at end of attack animations
        _isAttacking = false;
    }

    public void EndBattlecry()
    {
        // allows detection behavior to begin
        _battlecry = true;
    }

    void HurtEffect()
    {
        Time.timeScale = 0.25f;
    }

    void UndoHurtEffect()
    {
        Time.timeScale = 1;
    }
}
