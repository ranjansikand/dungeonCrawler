// Move backwards
// Called based on detected incoming attack

using UnityEngine;

public class MobDodge : MobBase
{
    Vector3 _directionToTarget;
    float _dodgeSpeed = 3.5f;

    public MobDodge(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        ControlMotion();
    }

    public override void UpdateState() {
        CheckSwitchStates();

        Ctx.transform.Translate(-_directionToTarget * _dodgeSpeed * Time.deltaTime);
    }

    public override void ExitState() {
        Ctx.Agent.enabled = true;
    }

    public override void CheckSwitchStates() {
        if (!Ctx.Dodging) {
            
            SwitchState(Factory.Waiting());
        }
    }

    public override void InitializeSubState() {}

    void ControlMotion()
    {
        // Setup conditions
        Ctx.Dodging = true;
        Ctx.Agent.enabled = false;
        Ctx.Animator.SetTrigger(Ctx.DodgeHash);
        
        // Calculate direction
        _directionToTarget = (Ctx.Target.position - Ctx.transform.position).normalized;

        // Prepare motion
        Ctx.transform.rotation = Quaternion.LookRotation(_directionToTarget);
    }
}
