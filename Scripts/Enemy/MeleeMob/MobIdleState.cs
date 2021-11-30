using UnityEngine;

public class MobIdleState : MobBaseState
{
    public MobIdleState(MobStateMachine currentContext, MobStateFactory enemyStateFactory)
     : base (currentContext, enemyStateFactory) {
        IsRootState = true;
    }

    public override void EnterState() {
        Debug.Log("Entered idle state.");
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Debug.Log("Exiting idle state");
    }

    public override void CheckSwitchStates() {
        if (Ctx.PlayerDetected) {
            SwitchState(Factory.Attack());
        } else {
            SwitchState(Factory.Patrol());
        }
    }

    public override void InitializeSubState() {}

}
