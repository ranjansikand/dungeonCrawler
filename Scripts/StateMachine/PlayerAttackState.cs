using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    Transform target;

    IEnumerator IAttackResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        Ctx.AttackCount = 0;
    }

    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        HandleMotion();
        HandleAttack();
    }

    public override void UpdateState() {
        if (target != null) Ctx.gameObject.transform.LookAt(target.position);
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.IsAttackPressed = false;
    }

    public override void InitializaSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsAttacking) {
            if (!Ctx.IsMovementPressed) {
                SwitchState(Factory.Idle());
            } else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
                SwitchState(Factory.Walk());
            } else {
                SwitchState(Factory.Run());
            }
        }
    }

    void HandleMotion()
    {
        RaycastHit hit;

        if (Physics.SphereCast(Ctx.gameObject.transform.position, 1, Ctx.gameObject.transform.forward, out hit, 5, 1 << 15)) {
            target = hit.collider.gameObject.transform;
        }
    }

    void HandleAttack()
    {
        // variable accessed by animation events
        Ctx.IsAttacking = true;
        // variable set by input

        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;

        // count attacks to transition to a specified animation
        Ctx.Animator.SetTrigger(Ctx.AttackingHash);
    }
}
