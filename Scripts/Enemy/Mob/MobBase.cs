public abstract class MobBase
{
    private bool _isRootState = false;
    private bool _isLeafState = false;
    private MobMachine _ctx;
    private MobFactory _factory;
    private MobBase _currentSubState;
    private MobBase _currentSuperState;

    protected bool IsRootState { set { _isRootState = value; }}
    protected bool IsLeafState { set { _isLeafState = value; }}
    protected MobMachine Ctx { get { return _ctx; }}
    protected MobFactory Factory {get {return _factory; }}

    public MobBase(MobMachine currentContext, MobFactory stateFactory) {
        _ctx = currentContext;
        _factory = stateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates() {
        UpdateState();
        if (_currentSubState != null) {
            _currentSubState.UpdateStates();
        }
    }

    protected void SwitchState(MobBase newState) {
        ExitState();

        newState.EnterState();
        if (_isRootState) {
            _ctx.CurrentState = newState;
        } else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(MobBase newSuperState) {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(MobBase newSubState) {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
        newSubState.EnterState();
    }

    protected void CloseSubState()
    {
        _currentSubState.ExitState();
        _currentSubState = null;
    }
}