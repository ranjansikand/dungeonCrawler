// State that controls the stagger for shielded enemies

using System.Collections;

public class BruteStaggered : BruteBaseState
{
    IEnumerator IEndStagger() {
        yield return Ctx.StaggerDuration;
        CheckSwitchStates();
    }
    public BruteStaggered(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
        // No sub-state
    }

    public override void EnterState() {
        // Start stagger animation
        Ctx.GuardBroken = false;
        Ctx.Animator.SetBool(Ctx.StaggerHash, true);
        Ctx.StartCoroutine(IEndStagger());
    }

    public override void UpdateState() {}

    public override void ExitState() {
        Ctx.CurrentPosture = Ctx.MaxPosture;
        Ctx.Animator.SetBool(Ctx.StaggerHash, false);
    }

    public override void CheckSwitchStates() {
        if (Ctx.IsDead) {
            SwitchState(Factory.Dead());
        } else {
            SwitchState(Factory.Detected());
        }
    }

    public override void InitializeSubState() {}
}
