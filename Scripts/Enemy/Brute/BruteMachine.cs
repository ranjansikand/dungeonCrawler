using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BruteMachine : MonoBehaviour, IDamagable
{
    // Components
    NavMeshAgent _agent;
    Animator _animator;
    Collider _bodyCollider;

    Transform _target;

    // Blocking state
    bool _isBlocking;
    int _remainingPosture;

    [Header("Ranges")]
    [SerializeField] float _sightRange;
    [SerializeField] float _patrolRange;
    [SerializeField] Vector3[] _waypoints;
    int _waypointIndex = -1;
    Vector3 _pinnedPosition;

    // Attacking state
    [Header("Attacking Parameters")]
    [SerializeField] Collider _weaponHitbox;  // for attacks
    [SerializeField] Collider _shieldHitbox;  // for attack and defense
    bool _isAttacking;


    [Header("Health and Damage")]
    [SerializeField] int _maxHealth; // max damage that can be taken
    [SerializeField] int _maxPosture; // damage taken before staggered
    int _currentHealth;

    BruteBaseState _currentState;
    BruteStateFactory _states;

    // Getters and Setters
    public NavMeshAgent Agent { get { return _agent; }}
    public Animator Animator { get { return _animator; }}

    // targeting and detection
    public Transform Target { get { return _target; } set { _target = value; }}
    public int LayerMask { get { return 1 << 10; }}

    public BruteBaseState CurrentState { get { return _currentState; } set { _currentState = value; }}
    
    public bool IsBlocking { get { return _isBlocking; } set { _isBlocking = value; }}
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; }}
    
    public Vector3 PinnedPosition { get { return _pinnedPosition; }}
    public Vector3[] Waypoints { get { return _waypoints; }}
    public int WaypointIndex { get { return _waypointIndex; } set { _waypointIndex = value; }}
    public float SightRange { get { return _sightRange; }}
    public float PatrolRange { get { return _patrolRange; }}

    void Awake()
    {
        // Fetch components
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _bodyCollider = GetComponent<Collider>();

        // Initialize state behavior
        _states = new BruteStateFactory(this);
        _currentState = _states.Searching();
        _currentState.EnterState();

        _pinnedPosition = transform.position;
    }

    void Update() 
    {
        _currentState.UpdateStates();
    }

    public void Damage(int damage) 
    {
        if (_isBlocking) _remainingPosture -= damage; 
        else _currentHealth -= damage;

        if (_remainingPosture <= 0) {
            Debug.Log("Guard broken!");
        }

        if (_currentHealth <= 0) {
            Debug.Log("Dead"); 
        }
    }

    public int CurrentHealth() {
        return _currentHealth;
    }

    public void ToggleWeaponHitbox() {
        // called from animation to toggle damage on and off
        _weaponHitbox.enabled = !_weaponHitbox.enabled;
    }

    public void ToggleShieldHitbox() {
        // called from animation to toggle damage on and off
        _shieldHitbox.enabled = !_shieldHitbox.enabled;
    }
}
