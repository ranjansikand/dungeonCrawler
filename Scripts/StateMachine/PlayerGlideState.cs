// Raises shield and protects player from damage
using UnityEngine;

public class PlayerGlideState : PlayerBaseState
{
    const float falling = 2.0f;
    const float gliding = 0.1f;

    public PlayerGlideState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        CheckGlide();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.IsGliding = false;
        Ctx.FallMultiplier = falling;
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsBlockPressed || !Ctx.IsGliding || Ctx.IsAttacking || Ctx.IsAttackPressed || Ctx.IsGrounded) {
            SwitchState(Factory.Standard());
        }
    }

    void CheckGlide()
    {
        // Seperate attack and block
        if (Ctx.IsGrounded) return; 

        Ctx.IsGliding = true;
        Ctx.FallMultiplier = gliding;
    }
}