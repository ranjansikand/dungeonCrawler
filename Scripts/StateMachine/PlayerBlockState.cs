// Raises shield and protects player from damage
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    public PlayerBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        CheckBlock();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.IsBlocking = false;
        Ctx.OnBlockEnded.Invoke();
        Ctx.Animator.SetBool(Ctx.IsBlockingHash, false);
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsBlockPressed || !Ctx.IsBlocking|| Ctx.IsAttacking || Ctx.IsAttackPressed) {
            SwitchState(Factory.Standard());
        }
    }

    void CheckBlock()
    {
        // Seperate attack and block
        if (Ctx.IsAttackPressed || Ctx.IsAttacking) return;

        Ctx.IsBlocking = true;
        Ctx.OnBlockStarted.Invoke();
        Ctx.Animator.SetBool(Ctx.IsBlockingHash, true);
    }
}