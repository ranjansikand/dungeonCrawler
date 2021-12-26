using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCircling : MobBase
{
    bool _retreating;

    IEnumerator IEndRetreat()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3.25f));

        _retreating = false;
    }

    public MobCircling(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        PickDestination();
    }

    public override void UpdateState() {
        CheckSwitchStates();

        Ctx.transform.LookAt(Ctx.Target);
    }

    public override void ExitState() {
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (!_retreating) {
            SwitchState(Factory.Chase());
        }
    }

    public override void InitializeSubState() {}

    void PickDestination()
    {
        _retreating = true;

        Vector3 dirAway = (Ctx.Target.position - Ctx.transform.position).normalized;

        Ctx.StartCoroutine(IEndRetreat());
        Ctx.Agent.SetDestination(dirAway * 7f);
    }
}
