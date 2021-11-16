using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void InitializeSubState() {
        if (Ctx.IsAttackPressed || Ctx.IsAttacking) {
            SetSubState(Factory.Attack());
        } else if (Ctx.IsDodgePressed || Ctx.IsDodging) {
            SetSubState(Factory.Dodge());
        } else {
            SetSubState(Factory.Standard());
        }
    }

    public override void CheckSwitchStates() {
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SwitchState(Factory.Run());
        } else if (Ctx.IsMovementPressed) {
            SwitchState(Factory.Walk());
        }
    }
}
