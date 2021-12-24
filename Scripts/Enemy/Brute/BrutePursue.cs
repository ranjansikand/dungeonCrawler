public class BrutePursue : BruteBaseState
{
    public BrutePursue(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {

    }

    public override void UpdateState() {
        CheckSwitchStates();

        Ctx.Agent.SetDestination(Ctx.Target.position);
    }

    public override void ExitState() {
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (Ctx.DistanceToTarget < Ctx.AttackRange) {
            SwitchState(Factory.Attack());
        }
    }

    public override void InitializeSubState() {}
}
