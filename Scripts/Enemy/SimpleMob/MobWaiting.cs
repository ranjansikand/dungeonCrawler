// State that handles all combat logic for mob

using System.Collections;
using UnityEngine;

public class MobWaiting : MobBase
{
    IEnumerator ICheckSwitchStates()
    {
        yield return new WaitForSeconds(Random.Range(0f, 3f));

        CheckSwitchStates();
    }

    public MobWaiting(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        Ctx.Agent.SetDestination(Ctx.transform.position);
        Ctx.Animator.SetBool(Ctx.BlockingHash, true);
        Ctx.StartCoroutine(ICheckSwitchStates());
    }

    public override void UpdateState() {
        if (Ctx.PlayerMachine.IsAttacking && Random.Range(0, 2) == 1) {
            Ctx.StopCoroutine(ICheckSwitchStates());
            SwitchState(Factory.Dodge());
        }
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.BlockingHash, false);
    }

    public override void CheckSwitchStates() {
        float distance = Ctx.Target!=null ? Vector3.Distance(Ctx.transform.position, Ctx.Target.position) : 20f;

        if (distance < Ctx.AttackRange) {
            SwitchState(Factory.Attack());
        } else if (distance < Ctx.ChaseRange) {
            SwitchState(Factory.Chase());
        } else {
            Ctx.Target = null;
        }
    }

    public override void InitializeSubState() {}
}
