using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    CharacterController _characterController;
    Animator _animator;
    PlayerInput _playerInput;

    int _isWalkingHash;
    int _isRunningHash;

    // movement variables
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement;   
    bool _isMovementPressed;
    bool _isRunPressed;

    // constants
    [SerializeField] float _walkMultiplier = 2.0f;
    [SerializeField] float _runMultiplier = 4.0f;
    int _zero = 0;

    // gravity
    float _gravity = -9.8f;
    float _groundedGravity = -0.05f;

    // jumping variables
    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    [SerializeField] float _maxJumpHeight = 4.0f;
    [SerializeField] float _maxJumpTime = 0.75f;
    bool _isGrounded;
    bool _isJumping = false;
    int _isJumpingHash;
    int _jumpCountHash;
    bool _requireNewJumpPress = false;
    int _jumpCount = 0;
    Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    Coroutine _currentJumpResetRoutine = null;

    // attack variables 
    bool _isAttackPressed = false;
    bool _isAttacking = false;
    int _attackingHash;
    int _attackCountHash;
    int _attackCount = 1;
    Coroutine _currentAttackResetRoutine = null;

    // dodge variables
    bool _isDodgePressed = false;
    bool _isDodging = false;
    int _dodgingHash;

    // blocking
    bool _isBlockPressed;
    bool _isBlocking;
    public delegate void OnBlockDelegate();
    public static OnBlockDelegate onBlockStarted;
    public static OnBlockDelegate onBlockEnded;

    // death
    bool isDead;

    // camera rotation
    Transform _reference; 
    float _speedSmoothVelocity;
    bool _lockedOn = false;
    Transform _target;

    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    public PlayerBaseState CurrentState { get {return _currentState;} set {_currentState = value; }}
    public Animator Animator { get { return _animator; }}
    public Transform Target { set { _target = value; }}
    public Coroutine CurrentJumpResetRoutine { get { return _currentJumpResetRoutine; } set { _currentJumpResetRoutine = value; }}
    public Coroutine CurrentAttackResetRoutine { get { return _currentAttackResetRoutine; } set { _currentAttackResetRoutine = value; }}
    public OnBlockDelegate OnBlockStarted { get { return onBlockStarted; }}
    public OnBlockDelegate OnBlockEnded { get { return onBlockEnded; }}
    public Dictionary<int, float> InitialJumpVelocities { get {return _initialJumpVelocities; }}
    public Dictionary<int, float> JumpGravities { get { return _jumpGravities; }}
    public int JumpCount { get { return _jumpCount; } set {_jumpCount = value; }}
    public int IsJumpingHash { get { return _isJumpingHash; }}
    public int IsWalkingHash { get { return _isWalkingHash; }}
    public int IsRunningHash { get { return _isRunningHash; }}
    public int JumpCountHash { get { return _jumpCountHash; }}
    public int AttackingHash { get { return _attackingHash; }}
    public int AttackCount { get { return _attackCount; } set { _attackCount = value; }}
    public int AttackCountHash { get { return _attackCountHash; }}
    public int IsDodgingHash { get { return _dodgingHash; }}
    public bool IsMovementPressed { get { return _isMovementPressed; }}
    public bool IsRunPressed { get { return _isRunPressed; }}
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; }}
    public bool IsJumping { set { _isJumping = value; }}
    public bool IsJumpPressed { get {return _isJumpPressed; }}
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; }}
    public bool IsAttackPressed { get { return _isAttackPressed; } set { _isAttackPressed = value; }}
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; }}
    public bool IsDodgePressed { get { return _isDodgePressed; } set { _isDodgePressed = value; }}
    public bool IsDodging { get { return _isDodging; } set { _isDodging = value; }}
    public bool IsDead { get {return isDead;} set {isDead = value; }}
    public bool IsBlockPressed { get { return _isBlockPressed; }}
    public bool IsBlocking { get { return _isBlocking; } set { _isBlocking = value; }}
    public float GroundedGravity { get { return _groundedGravity; }}
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; }}
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; }}
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; }}
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; }}
    public float RunMultiplier { get { return _runMultiplier; }}
    public float WalkMultiplier { get { return _walkMultiplier; }}
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; }}
    public Vector3 AppliedMovement { get { return _appliedMovement; } set { _appliedMovement = value; }}
    public Transform Reference { get { return _reference; }}

    void Awake()
    {
        _playerInput = new PlayerInput(); 
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");
        _attackingHash = Animator.StringToHash("attacking");
        _attackCountHash = Animator.StringToHash("attackCount");
        _dodgingHash = Animator.StringToHash("isDodging");

        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Run.performed += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
        _playerInput.CharacterControls.Jump.performed += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;
        _playerInput.CharacterControls.Menu.performed += OnMenu;
        _playerInput.CharacterControls.Attack.performed += OnAttack;
        _playerInput.CharacterControls.Attack.canceled += OnAttack;
        _playerInput.CharacterControls.Dodge.performed += OnDodge;
        _playerInput.CharacterControls.Dodge.canceled -= OnDodge;
        _playerInput.CharacterControls.LockOn.performed += OnLockOn;
        _playerInput.CharacterControls.LockOn.canceled -= OnLockOn;
        _playerInput.CharacterControls.Block.performed += OnBlock;
        _playerInput.CharacterControls.Block.canceled += OnBlock;

        _reference = new GameObject().transform;

        SetupJumpVariables();
        StartCoroutine(CheckForGround());
    }

    void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        _jumpGravities.Add(0, _gravity);
        _jumpGravities.Add(1, _gravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);
    }

    IEnumerator CheckForGround()
    {   
        while (true)
        {
            if (Physics.Raycast(transform.position, -1 * transform.up, 0.25f, 1 << 7)) {
                _isGrounded = true;
            } else {
                _isGrounded = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            HandleRotation();
            _currentState.UpdateStates();
            _characterController.Move(_appliedMovement * Time.deltaTime);
        }
    }

    void HandleRotation()
    {
        _reference.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);

        if (_lockedOn) {
            // strafe movement
            Vector3 targetDirection = _target.position - transform.position;
            Quaternion currentRotation = transform.rotation;
    
            if (_isMovementPressed) {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection.normalized);
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 15.0f * Time.deltaTime);
            }
            return;
        }
        if (!_isAttacking && !_isDodging) {
            // normal movement
            if (_isMovementPressed) {
                float targetRotation = Mathf.Atan2(_currentMovementInput.x, _currentMovementInput.y) * Mathf.Rad2Deg + _reference.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _speedSmoothVelocity, 0.1f);
            }
        }
    }

    void ResetAttack()
    {
        _isAttacking = false;
    }

    void ResetDodge()
    {
        _isDodging = false;
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != _zero || _currentMovementInput.y != _zero;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnMenu(InputAction.CallbackContext context)
    {
        Inventory.instance.ToggleInventory();
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
    }

    void OnDodge(InputAction.CallbackContext context)
    {
        _isDodgePressed = true;
    }

    void OnLockOn(InputAction.CallbackContext context)
    {
        CameraManager.instance.OnLockOn(_lockedOn);
        _lockedOn = _target != null ? true : false;
    }

    void OnBlock(InputAction.CallbackContext context)
    {
        _isBlockPressed = context.ReadValueAsButton();
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
