using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCircling : MobBase
{
    Vector3 _destination;
    bool _circling;

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

        if (Ctx.Agent.remainingDistance < 1) {
            _circling = false;
        }
    }

    public override void ExitState() {
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (!_circling) {
            SwitchState(Factory.Chase());
        }
    }

    public override void InitializeSubState() {}

    void PickDestination()
    {
        _circling = true;

        Vector3 dirFromTarget = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f,1f)).normalized;
        _destination = dirFromTarget * Ctx.CircleRange;

        Ctx.Agent.SetDestination(_destination);
    }
}
