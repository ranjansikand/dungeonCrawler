using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BruteBaseState
{
    
    private bool _isRootState = false;
    private bool _isLeafState = false;
    private BruteMachine _ctx;
    private BruteStateFactory _factory;
    private BruteBaseState _currentSubState;
    private BruteBaseState _currentSuperState;

    protected bool IsRootState { set { _isRootState = value; }}
    protected bool IsLeafState { set { _isLeafState = value; }}
    protected BruteMachine Ctx { get { return _ctx; }}
    protected BruteStateFactory Factory {get {return _factory; }}

    public BruteBaseState(BruteMachine currentContext, BruteStateFactory stateFactory) {
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

    protected void SwitchState(BruteBaseState newState) {
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

    protected void SetSuperState(BruteBaseState newSuperState) {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(BruteBaseState newSubState) {
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
