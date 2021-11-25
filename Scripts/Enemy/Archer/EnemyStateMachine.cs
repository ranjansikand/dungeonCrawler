using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour, IDamagable
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

    // states
    EnemyBaseState _currentState;
    EnemyStateFactory _states;

    public Animator Animator { get { return _animator; }}
    public NavMeshAgent Agent { get { return _agent; }}
    public Collider Collider { get {return _collider; }}
    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; }}
    public Coroutine CurrentPatrolRoutine { get { return _currentPatrolRoutine; } set { _currentPatrolRoutine = value; }}
    public Transform Target { get { return _target; } set { _target = value; }}

    public bool IsDead { get { return _isDead; }}
    public bool PlayerDetected { get { return _playerDetected; } set { _playerDetected = value; }}
    public int IsAttackingHash { get { return _isAttackingHash; }}
    public float SightRange { get { return _sightRange; }}

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();

        _isAttackingHash = Animator.StringToHash("detected");
        _isHurtHash = Animator.StringToHash("hurt");
        _isDeadHash = Animator.StringToHash("dead");

        _states = new EnemyStateFactory(this);
        _currentState = _states.Patrol();
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
