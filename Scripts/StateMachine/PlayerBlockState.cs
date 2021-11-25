// Block makes it so the player is un-injurable for a period of time

public class PlayerBlockState : PlayerBaseState
{
    public PlayerBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        Ctx.IsBlocking = true;
        Ctx.OnBlockStarted.Invoke();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.IsBlocking = false;
        Ctx.OnBlockEnded.Invoke();
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsBlockPressed) {
            SwitchState(Factory.Standard());
        }
    }
}
