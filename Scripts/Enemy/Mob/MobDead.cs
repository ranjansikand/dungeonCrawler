using UnityEngine;
public class MobDead : MobBase
{
    public MobDead(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
    }

    public override void EnterState() {
        Debug.Log("DEAD");
        Ctx.Target = null;
        Ctx.Animator.SetTrigger(Ctx.DeadHash);
    }

    public override void UpdateState() {
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}
}
