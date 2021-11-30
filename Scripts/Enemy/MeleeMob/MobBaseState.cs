public abstract class MobBaseState
{
    private bool _isRootState = false;
    private bool _isLeafState = false;
    private MobStateMachine _ctx;
    private MobStateFactory _factory;
    private MobBaseState _currentSubState;
    private MobBaseState _currentSuperState;

    protected bool IsRootState { set { _isRootState = value; }}
    protected bool IsLeafState { set { _isLeafState = value; }}
    protected MobStateMachine Ctx { get { return _ctx; }}
    protected MobStateFactory Factory {get {return _factory; }}

    public MobBaseState(MobStateMachine currentContext, MobStateFactory enemyStateFactory) {
        _ctx = currentContext;
        _factory = enemyStateFactory;
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

    protected void SwitchState(MobBaseState newState) {
        ExitState();

        newState.EnterState();
        if (_isRootState) {
            _ctx.CurrentState = newState;
        } else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(MobBaseState newSuperState) {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(MobBaseState newSubState) {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}