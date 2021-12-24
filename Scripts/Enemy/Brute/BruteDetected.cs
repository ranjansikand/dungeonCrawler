using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteDetected : BruteBaseState
{
    public BruteDetected(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.Agent.SetDestination(Ctx.transform.position);
        // Play detected effect
        if (!Ctx.Battlecry) Ctx.Animator.SetTrigger(Ctx.DetectedHash);

        Debug.Log("Detected target");
    }

    public override void UpdateState() {
        Ctx.DistanceToTarget = Vector3.Distance(Ctx.transform.position, Ctx.Target.position);
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (Ctx.DistanceToTarget > Ctx.AggroRange
                || Ctx.Target == null) {
            SwitchState(Factory.Searching());
        } 
        else if (Ctx.GuardBroken) {
            SwitchState(Factory.Staggered());
        } 
        else if (Ctx.IsDead) {
            SwitchState(Factory.Dead());
        }
    }

    public override void InitializeSubState() {
        if (Ctx.Battlecry) SetSubState(Factory.Pursue());
    }
}
