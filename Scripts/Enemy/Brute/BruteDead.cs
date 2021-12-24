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
        // Start death animation
        Ctx.BodyCollider.enabled = false;
    }

    public override void UpdateState() {}

    public override void ExitState() {}

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}
}
