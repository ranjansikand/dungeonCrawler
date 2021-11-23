using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    bool cachedAttack = false;

    IEnumerator IAttackResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        Ctx.AttackCount = 1;
    }

    IEnumerator ISwitchToStandard()
    {
        float value = 1;

        for (int i=0; i<30; i++) {
            value -= 1f/30;
            Ctx.Animator.SetFloat("Blend", value);
            yield return null;
        }

        SwitchState(Factory.Standard());
    }

    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        HandleAttack();
    }

    public override void UpdateState() {
        HandleMotion();
        CheckSwitchStates();

        if (Ctx.IsAttackPressed && !cachedAttack) {
            cachedAttack = true;
        }
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.AttackingHash, false);
        Ctx.IsAttackPressed = false;

        if (!cachedAttack) Ctx.CurrentAttackResetRoutine = Ctx.StartCoroutine(IAttackResetRoutine());
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsAttacking) {
            if (cachedAttack) {
                SwitchState(Factory.Attack());
            }
            else if (Ctx.IsDodgePressed) {
                SwitchState(Factory.Dodge());
            } else {
                SwitchState(Factory.Standard());
                // Ctx.StartCoroutine(ISwitchToStandard());
            }
        }
    }

    void HandleMotion()
    {
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    void HandleAttack()
    {
        if (Ctx.CurrentAttackResetRoutine != null) Ctx.StopCoroutine(Ctx.CurrentAttackResetRoutine);

        // set variables
        Ctx.IsAttacking = true;
        Ctx.IsAttackPressed = false;

        // count attacks to transition to a specified animation
        Ctx.Animator.SetBool(Ctx.AttackingHash, true);
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, Ctx.AttackCount);

        if (Ctx.AttackCount == 3) Ctx.AttackCount = 1;
        else Ctx.AttackCount += 1;
    }
}
