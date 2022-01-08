public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        InitializeSubState();
    }

    public override void EnterState() {
        if (Ctx.Animator.GetBool(Ctx.IsWalkingHash) || Ctx.Animator.GetBool(Ctx.IsRunningHash)) {
            Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
            Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        }
        
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState() {
        CheckSwitchStates();
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
        } if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SwitchState(Factory.Run());
        } else if (Ctx.IsMovementPressed) {
            SwitchState(Factory.Walk());
        }
    }
}
