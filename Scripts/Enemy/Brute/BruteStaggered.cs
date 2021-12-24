using System.Collections;

public class BruteStaggered : BruteBaseState
{
    bool _postureRestored;

    IEnumerator IEndStagger()
    {
        yield return Ctx.StaggerDuration;

        _postureRestored = true;
    }
    public BruteStaggered(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
        // No sub-state
    }

    public override void EnterState() {
        // Start stagger animation

        _postureRestored = false;
        Ctx.StartCoroutine(IEndStagger());
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.CurrentPosture = Ctx.MaxPosture;
        // Stop animation
    }

    public override void CheckSwitchStates() {
        if (_postureRestored) {
            SwitchState(Factory.Detected());
        } else if (Ctx.IsDead) {
            SwitchState(Factory.Dead());
        }
    }

    public override void InitializeSubState() {}
}
