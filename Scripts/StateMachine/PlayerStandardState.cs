// State that handles transitions in and out of blocking
// Has no native behavior

public class PlayerStandardState : PlayerBaseState
{
    public PlayerStandardState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {}

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (Ctx.IsBlockPressed) {
            if (!Ctx.IsGrounded) {
                SwitchState(Factory.Glide());
            } else {
                SwitchState(Factory.Block());
            }
        }
    }
}
