using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChaseState : MobBaseState
{
    WaitForSeconds _delay = new WaitForSeconds(0.1f);
    bool _chasing = true;

    public MobChaseState(MobStateMachine currentContext, MobStateFactory enemyStateFactory)
     : base (currentContext, enemyStateFactory) {
        IsRootState = true;
    }

    public override void EnterState() {
        Ctx.StartCoroutine(DistanceToTarget());
    }

    public override void UpdateState() {
        Ctx.Agent.SetDestination(Ctx.Target.position);
    }

    public override void ExitState() {
        _chasing = false;
    }

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}

    IEnumerator DistanceToTarget() {
        while (_chasing) {
            float distance = Vector3.Distance(Ctx.Target.position, Ctx.transform.position);

            if (distance < Ctx.AttackRadius) {
                SwitchState(Factory.Attack());
            } else if (distance > Ctx.ChaseRadius) {
                SwitchState(Factory.Patrol());
            } else {
                Ctx.Agent.SetDestination(Ctx.Target.position);
            }

            yield return _delay;
        }
    }
}
