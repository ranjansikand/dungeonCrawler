using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    IEnumerator IAttackResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        Ctx.AttackCount = 0;
    }

    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        HandleAttack();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.AttackingHash, false);
        Ctx.IsAttackPressed = false;
        Ctx.CurrentAttackResetRoutine = Ctx.StartCoroutine(IAttackResetRoutine());
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

    void HandleAttack()
    {
        if (Ctx.JumpCount < 2 && Ctx.CurrentJumpResetRoutine != null)
        {
            Ctx.StopCoroutine(Ctx.CurrentJumpResetRoutine);
        }

        // variable accessed by animation events
        Ctx.IsAttacking = true;
        // variable set by input

        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;

        // count attacks to transition to a specified animation
        Ctx.Animator.SetBool(Ctx.AttackingHash, true);

        Ctx.AttackCount++;
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, Ctx.AttackCount);
    }
}
