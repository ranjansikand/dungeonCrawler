using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
    }

    public override void UpdateState() {
        CheckSwitchStates();
        Ctx.AppliedMovement = Ctx.Reference.forward * Ctx.CurrentMovementInput.y * Ctx.RunMultiplier
           + Ctx.Reference.right * Ctx.CurrentMovementInput.x * Ctx.RunMultiplier + 
           new Vector3(0, Ctx.AppliedMovementY, 0);
    }

    public override void ExitState() {}

    public override void InitializeSubState() {
        if (Ctx.IsBlocking || Ctx.IsBlockPressed) {
            SetSubState(Factory.Block());
        } else {
            SetSubState(Factory.Standard());
        }
    }

    public override void CheckSwitchStates() {
        if (Ctx.IsAttackPressed || Ctx.IsAttacking) {
            SwitchState(Factory.Attack());
        } else if (Ctx.IsDodgePressed || Ctx.IsDodging) {
            SwitchState(Factory.Dodge());
        }else if (!Ctx.IsMovementPressed) {
            SwitchState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SwitchState(Factory.Walk());
        }
    }
}
