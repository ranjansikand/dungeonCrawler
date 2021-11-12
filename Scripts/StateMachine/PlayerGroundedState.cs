using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        IsRootState = true;
        InitializaSubState();
    }

    public override void EnterState() {
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
        Ctx.AppliedMovementY = Ctx.GroundedGravity;
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void InitializaSubState() {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Walk());
        } else if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SetSubState(Factory.Run());
        } else {
            SetSubState(Factory.Attack());
        }
    }

    public override void CheckSwitchStates() {
        if ((Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress) || !Ctx.IsGrounded) {
            SwitchState(Factory.Jump());
        }
    }
}
