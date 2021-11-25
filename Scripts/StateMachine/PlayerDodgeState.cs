using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    Vector2 dodgeDirection;

    public PlayerDodgeState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        InitializeSubState();
    }

    public override void EnterState() {
        HandleDodge();
    }

    public override void UpdateState() {
        CheckSwitchStates();

        Ctx.AppliedMovement = Ctx.Reference.forward * dodgeDirection.y * Ctx.RunMultiplier
                + Ctx.Reference.right * dodgeDirection.x * Ctx.RunMultiplier + 
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
        if (!Ctx.IsDodging)
        {
           if (Ctx.IsAttackPressed || Ctx.IsAttacking) {
                SwitchState(Factory.Attack());
            } else if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
                SwitchState(Factory.Run());
            } else if (Ctx.IsMovementPressed) {
                SwitchState(Factory.Walk());
            } else {
                SwitchState(Factory.Idle());
            }
        }
    }

    void HandleDodge()
    {
        Ctx.IsDodging = true;
        Ctx.IsDodgePressed = false;

        if (Ctx.CurrentMovementInput == Vector2.zero) {
            // don't dodge without directional input
            Ctx.IsDodging = false;
        } else {
            // lock-in direction on state enter
            dodgeDirection = Ctx.CurrentMovementInput;
            // start animation
            Ctx.Animator.SetTrigger(Ctx.IsDodgingHash);
        }
    }
}
