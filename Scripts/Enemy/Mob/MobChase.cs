using System.Collections;
using UnityEngine;

public class MobChase : MobBase
{
    WaitForSeconds delay = new WaitForSeconds(0.5f);

    Vector3 _pointToPlayer;

    public MobChase(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        Ctx.StartCoroutine(IHandlePursuit());
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.StopCoroutine(IHandlePursuit());
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (Vector3.Distance(Ctx.Target.position, Ctx.transform.position) < Ctx.AttackRange) {
            // Check if anything is blocking sightline
            SwitchState(Factory.Attack());
        }
    }

    public override void InitializeSubState() {}

    IEnumerator IHandlePursuit()
    {
        while (true) {
            yield return delay;

            if (Ctx.Target != null) {
                float distance = Vector3.Distance(Ctx.Target.position, Ctx.transform.position);

                if (distance > Ctx.ChaseRange || distance < Ctx.AttackRange) {
                    // if player is too far away or too close
                    Ctx.Agent.SetDestination(Ctx.transform.position);
                    Ctx.Target = null;
                } else {
                    _pointToPlayer = (Ctx.Target.position - Ctx.transform.position).normalized;
                    Ctx.Agent.SetDestination(Ctx.Target.position);
                }
            }
        }
    }
}
