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
        Ctx.Status.UpdateSprite(Ctx.Alarmed);
        Ctx.PlayerMachine = Ctx.Target.GetComponent<PlayerStateMachine>();
    }

    public override void UpdateState() {
        CheckSwitchStates();

        if (!Ctx.Agent.enabled && !Ctx.Dodging) Ctx.Agent.enabled = true;
    }

    public override void ExitState() {
        CloseSubState();
        Ctx.PlayerMachine = null;
    }

    public override void CheckSwitchStates() {
        if (Ctx.IsDead) {
            SwitchState(Factory.Dead());
        } else if (Ctx.Target == null) {
            SwitchState(Factory.Searching());
        }
    }

    public override void InitializeSubState() {
        SetSubState(Factory.Chase());
    }
}
