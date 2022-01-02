// State that controls enemy death
// Prevents erroneous post-death behaviors
using UnityEngine;

public class MobDead : MobBase
{
    public MobDead(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
    }

    public override void EnterState() {
        Ctx.Target = null;
        Ctx.Agent.enabled = false;

        // Stop all other animations
        foreach (AnimatorControllerParameter parameter in Ctx.Animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                Ctx.Animator.SetBool(parameter.name, false);
        }

        Ctx.Animator.SetTrigger(Ctx.DeadHash);
        
    }

    public override void UpdateState() {}

    public override void ExitState() {}

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}
}
