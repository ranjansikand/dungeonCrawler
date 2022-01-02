// Move to a new location

using UnityEngine;

public class MobDodge : MobBase
{
    public MobDodge(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        ControlMotion();
    }

    public override void UpdateState() {
        CheckSwitchStates();

        Ctx.transform.LookAt(new Vector3(Ctx.Target.position.x,0,Ctx.Target.position.z));
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (Ctx.Agent.remainingDistance < 1f) {
            SwitchState(Factory.Waiting());
        }
    }

    public override void InitializeSubState() {}

    void ControlMotion()
    {
        Ctx.Agent.Warp(Ctx.Target.position + new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)));
    }
}
