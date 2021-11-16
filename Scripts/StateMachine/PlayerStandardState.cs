using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandardState : PlayerBaseState
{
    public PlayerStandardState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {}

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (Ctx.IsAttackPressed) {
            SwitchState(Factory.Attack());
        } else if (Ctx.IsDodgePressed) {
            SwitchState(Factory.Dodge());
        }
    }
}
