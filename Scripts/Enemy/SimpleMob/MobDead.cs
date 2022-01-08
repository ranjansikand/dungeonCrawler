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

        Ctx.Status.UpdateSprite(Ctx.Dead);

        // Stop all other animations
        foreach (AnimatorControllerParameter parameter in Ctx.Animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                Ctx.Animator.SetBool(parameter.name, false);
        }

        // Transition to death
        Ctx.Animator.SetTrigger(Ctx.DeadHash);
        // Select which death animation to use. Hash not needed as this is only called once
        Ctx.Animator.SetInteger("death", Random.Range(1,6));
    }

    public override void UpdateState() {}

    public override void ExitState() {}

    public override void CheckSwitchStates() {}

    public override void InitializeSubState() {}
}
