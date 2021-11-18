public class EnemyStandardState : EnemyBaseState
{
    public EnemyStandardState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
        : base (currentContext, enemyStateFactory) {
            IsLeafState = true;
        }

    public override void EnterState() {}

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (Ctx.PlayerDetected) {
            SwitchState(Factory.Attack());
        }
    }
}
