using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteDead : BruteBaseState
{
    public BruteDead(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
        // No sub-state
    }

    public override void EnterState() {
        Ctx.Agent.destination = Ctx.transform.position;
        Debug.Log("Current position: " + Ctx.transform.position + " | Destination: " + Ctx.Agent.destination);

        // Start death animation
        Ctx.BodyCollider.enabled = false;
        Ctx.Agent.enabled = false;
        Ctx.Animator.SetTrigger("die");
    }

    public override void UpdateState() {}

    public override void ExitState() {}

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}
}
