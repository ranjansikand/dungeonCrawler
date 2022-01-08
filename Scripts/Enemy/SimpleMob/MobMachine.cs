using UnityEngine;
using UnityEngine.AI;

public class MobMachine : MonoBehaviour, IDamagable
{
    // Components
    NavMeshAgent _agent;
    Animator _animator;
    Collider _bodyCollider;

    // Player or target from Searching state
    Transform _target;
    PlayerStateMachine _playerMachine;

    // Attacking
    [SerializeField] Collider _weaponHitbox;
    bool _attacking;
    bool _dodging;

    // Detection and radii
    [SerializeField] float _sightRange;
    [SerializeField] float _chaseRange;
    [SerializeField] float _attackRange;
    [SerializeField] int _numberOfAttacks;

    // Detection feedbacks
    [Header("Sprites")]
    [SerializeField] StatusSprite _status;
    [SerializeField] Sprite _confused;
    [SerializeField] Sprite _alarmed;
    [SerializeField] Sprite _dead;

    // animation hashes
    int _attackCountHash;
    int _hurtHash;
    int _deadHash;
    int _dodgeHash;
    int _blockingHash;

    Vector3 _anchorPosition;

    // health and damage functions
    [Header("Health")]
    [SerializeField] int _maxHealth;
    [SerializeField] int _hurtThreshold;

    [Header("Effects")]
    [SerializeField] ParticleSystem _bloodSplatter;
    [SerializeField] ParticleSystem _hit;
    [SerializeField] float _deathSlowMo = 0.15f;

    bool _isDead = false, _isRecovering = false;
    int _currentHealth;
    WaitForSeconds delay = new WaitForSeconds(0.1f);

    MobBase _currentState;
    MobFactory _states;

    // general references
    public NavMeshAgent Agent { get { return _agent; }}
    public MobBase CurrentState { get { return _currentState; } set { _currentState = value; }}
    public Transform Target { get { return _target; } set { _target = value; }}
    public PlayerStateMachine PlayerMachine { get { return _playerMachine; } set { _playerMachine = value; }}
    public bool IsDead { get { return _isDead; }}
    public Vector3 Anchor { get { return _anchorPosition; }}

    // Sprites
    public StatusSprite Status { get { return _status; }}
    public Sprite Confused { get { return _confused; }}
    public Sprite Alarmed { get { return _alarmed; }}
    public Sprite Dead { get { return _dead; }}

    // animation references
    public Animator Animator { get { return _animator; }}
    public int AttackCountHash { get { return _attackCountHash; }}
    public int DeadHash { get { return _deadHash; }}
    public int DodgeHash { get { return _dodgeHash; }}
    public int BlockingHash { get { return _blockingHash; }}

    // state-specific references
    public bool Attacking { get { return _attacking; } set { _attacking = value; }}
    public bool Dodging { get { return _dodging; } set { _dodging = value; }}
    public int LayerMask { get { return 1 << 10; }}
    public int NumberOfAttacks { get { return _numberOfAttacks; }}
    public float SightRange { get { return _sightRange; }}
    public float ChaseRange { get { return _chaseRange; }}
    public float AttackRange { get { return _attackRange; }}

    public string currentState;

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
        _dodgeHash = Animator.StringToHash("dodge");
        _blockingHash = Animator.StringToHash("blocking");

        // assign variables
        _currentHealth = _maxHealth;
        _anchorPosition = transform.position;
    }

    void Update()
    {
        _currentState.UpdateStates();

        currentState = _currentState.Substate.ToString();
    }

    // Use-specific methods
    void AttackReset() {
        // called from animation to end attack cycle
        _attacking = false;
        Animator.SetInteger(_attackCountHash, 0);
    }

    // Health functions
    public void Damage(int damage) {
        if (!_isRecovering) {
            _isRecovering = true;
            _currentHealth -= damage;

            _bloodSplatter.Stop();
            _bloodSplatter.Play();

            _hit.Play();

            if (_currentHealth > 0) {
                if (damage > _hurtThreshold && !Attacking) Animator.SetTrigger(_hurtHash);
            } else {
                _isDead = true; 
                _bodyCollider.enabled = false;
                HurtEffect();
                Invoke("UndoHurtEffect", _deathSlowMo);
            }
            Invoke(nameof(EndRecovery), 0.25f);
        }
    }

    public void ReenableAgent() {
        if (!_agent.enabled) _agent.enabled = true;
    }

    public int CurrentHealth() {
        return _currentHealth;
    }

    public void EndRecovery()
    {
        _isRecovering = false;
    }

    public void EnableHitbox()
    {
        // Called during the beginning of attack animations
        _weaponHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        // Called during the end of attack animations
        _weaponHitbox.enabled = false;
    }

    public void EndDodge()
    {
        _dodging = false;
    }

    void HurtEffect()
    {
        // Called at time of death
        Time.timeScale = 0.25f;
    }

    void UndoHurtEffect()
    {
        // Called at the end of death
        Time.timeScale = 1;
    }
}
