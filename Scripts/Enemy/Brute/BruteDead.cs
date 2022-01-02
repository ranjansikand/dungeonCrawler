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
        Ctx.Agent.enabled = false;

        // turn off all animations
        foreach (AnimatorControllerParameter parameter in Ctx.Animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                Ctx.Animator.SetBool(parameter.name, false);
        }

        // Exit stagger if still staggered
        if (Ctx.Animator.GetBool(Ctx.StaggerHash)) Ctx.Animator.SetBool(Ctx.StaggerHash, false);
        Ctx.Animator.SetTrigger("die");
    }

    public override void UpdateState() {}

    public override void ExitState() {}

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}
}
