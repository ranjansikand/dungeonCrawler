// Controls combat and post-detection behavior
// SubStates: Attack, Chase

public class MobDetected : MobBase
{
    public MobDetected(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        // Play any detected effects or animations
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        CloseSubState();
    }

    public override void CheckSwitchStates() {
        if (Ctx.IsDead) {
            SwitchState(Factory.Dead());
        }
        else if (Ctx.Target == null) {
            SwitchState(Factory.Searching());
        }
    }

    public override void InitializeSubState() {
        SetSubState(Factory.Chase());
    }
}
