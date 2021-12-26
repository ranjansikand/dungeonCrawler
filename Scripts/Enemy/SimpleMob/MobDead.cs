// State that controls enemy death
// Prevents erroneous post-death behaviors

public class MobDead : MobBase
{
    public MobDead(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
    }

    public override void EnterState() {
        Ctx.Target = null;
        Ctx.Animator.SetTrigger(Ctx.DeadHash);
        Ctx.Agent.enabled = false;
    }

    public override void UpdateState() {}

    public override void ExitState() {}

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}
}
