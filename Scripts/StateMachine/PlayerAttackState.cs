using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    bool cachedAttack = false;

    IEnumerator IAttackResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        Ctx.AttackCount = 1;
    }

    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        InitializeSubState();
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

    public override void InitializeSubState() {
        if (Ctx.IsBlocking || Ctx.IsBlockPressed) {
            SetSubState(Factory.Block());
        } else {
            SetSubState(Factory.Standard());
        }
    }

    public override void CheckSwitchStates() {
        if (!Ctx.IsAttacking) {
            if (cachedAttack) {
                SwitchState(Factory.Attack());
            } else if (Ctx.IsDodgePressed) {
                SwitchState(Factory.Dodge());
            } else if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
                SwitchState(Factory.Run());
            } else if (Ctx.IsMovementPressed) {
                SwitchState(Factory.Walk());
            } else {
                SwitchState(Factory.Idle());
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
