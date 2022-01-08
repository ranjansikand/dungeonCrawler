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
    protected MobFactory Factory {get { return _factory; }}
    public MobBase Substate { get { return _currentSubState; }}

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
            _currentSubState.UpdateState();
        }
    }

    protected void SwitchState(MobBase newState) {
        ExitState();

        newState.EnterState();
        if (_isRootState) {
            _currentSuperState = null;
            _ctx.CurrentState = newState;
        } else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(newState);
            _currentSubState = null;
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
        if (_currentSubState.Substate != null) _currentSubState.CloseSubState();
        _currentSubState.ExitState();
        _currentSubState = null;
    }


}