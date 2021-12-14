using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrutePatrol : BruteBaseState
{
    Vector3 randomDestination;

    public BrutePatrol(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        SetDestination();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (Vector3.Distance(randomDestination, Ctx.transform.position) <= 1)
        {
            SwitchState(Factory.Looking());
        }
    }

    public override void InitializeSubState() {}

    void SetDestination()
    {
        if (Ctx.Waypoints.Length > 0) {
            if (Ctx.WaypointIndex == -1 || Ctx.WaypointIndex == Ctx.Waypoints.Length) {
                Ctx.WaypointIndex = 0;
            } else {
                Ctx.WaypointIndex++;
            }
            Ctx.Agent.SetDestination(Ctx.Waypoints[Ctx.WaypointIndex]);
            return;
        }

        randomDestination = 
            Ctx.PinnedPosition +
            new Vector3(Random.Range(-Ctx.PatrolRange, Ctx.PatrolRange), 0, 
            Random.Range(-Ctx.PatrolRange, Ctx.PatrolRange));
        Ctx.Agent.SetDestination(randomDestination);
    }
}
