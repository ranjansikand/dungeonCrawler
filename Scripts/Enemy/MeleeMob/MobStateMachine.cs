using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobStateMachine : MonoBehaviour, IDamagable
{
    NavMeshAgent _agent;
    Animator _animator;
    Collider _collider;
   

    // animator hashes
    int _isAttackingHash;
    int _isHurtHash;
    int _isDeadHash;

    Transform _target;

    // health
    [SerializeField] int _health;
    [Tooltip("Damage needed to stagger")][SerializeField] int _hurtThreshold;
    bool _isDead;
    WaitForSeconds delay = new WaitForSeconds(0.1f);

    // patrolling
    Coroutine _currentPatrolRoutine;
    [SerializeField] float _sightRange;
    bool _playerDetected;

    // Attacking
    [SerializeField] float _attackRadius = 3f;
    [SerializeField] float _chaseRadius = 10f;
    [SerializeField] int _numberOfAttacks;
    bool _isAttacking;

    // states
    MobBaseState _currentState;
    MobStateFactory _states;

    public Animator Animator { get { return _animator; }}
    public NavMeshAgent Agent { get { return _agent; }}
    public Collider Collider { get {return _collider; }}
    public MobBaseState CurrentState { get { return _currentState; } set { _currentState = value; }}
    public Coroutine CurrentPatrolRoutine { get { return _currentPatrolRoutine; } set { _currentPatrolRoutine = value; }}
    public Transform Target { get { return _target; } set { _target = value; }}

    public bool IsDead { get { return _isDead; }}
    public bool PlayerDetected { get { return _playerDetected; } set { _playerDetected = value; }}
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; }}
    public int IsAttackingHash { get { return _isAttackingHash; }}
    public int NumberOfAttacks { get { return _numberOfAttacks; }}
    public float SightRange { get { return _sightRange; }}
    public float AttackRadius { get { return _attackRadius; }}
    public float ChaseRadius { get { return _chaseRadius; }}

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();

        _isAttackingHash = Animator.StringToHash("attack");
        _isHurtHash = Animator.StringToHash("hurt");
        _isDeadHash = Animator.StringToHash("dead");

        _states = new MobStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState(); 

        StartCoroutine(CheckHealth());
    }

    void Update()
    {
        if (!IsDead) {
            _currentState.UpdateStates();
        }
    }

    IEnumerator CheckHealth() {
        while (true) {
            if (_health <= 0) {
                Debug.Log("Dead");
                Dead();
            }
            yield return delay;
        }
    }

    void Dead()
    {
        StopAllCoroutines();
        _isDead = true;

        _collider.enabled = false;
        _animator.SetTrigger(_isDeadHash);
    }

    public void Damage(int damage)
    {
        _health -= damage;

        if (damage > _hurtThreshold && _health > 0) _animator.SetTrigger(_isHurtHash);
    }

    public int MaxHealth()
    {
        return _health;
    }
}
