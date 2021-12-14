using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
    }

    public override void UpdateState() {
        CheckSwitchStates();
        // Ctx.AppliedMovementX = Camera.main.transform.forward.x * Ctx.CurrentMovementInput.x * Ctx.WalkMultiplier;
        Ctx.AppliedMovement = Ctx.Reference.forward * Ctx.CurrentMovementInput.y * Ctx.WalkMultiplier
           + Ctx.Reference.right * Ctx.CurrentMovementInput.x * Ctx.WalkMultiplier + 
           new Vector3(0, Ctx.AppliedMovementY, 0);

        Ctx.Animator.SetFloat(Ctx.DirXHash, Ctx.CurrentMovementInput.x);
        Ctx.Animator.SetFloat(Ctx.DirYHash, Ctx.CurrentMovementInput.y);
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
        } else if (!Ctx.IsMovementPressed) {
            SwitchState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SwitchState(Factory.Run());
        }
    }
}
