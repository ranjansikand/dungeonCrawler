using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    float timeElapsed;

    IEnumerator IAttackResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        Ctx.AttackCount = 0;
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
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.AttackingHash, false);
        Ctx.IsAttackPressed = false;

        Ctx.CurrentAttackResetRoutine = Ctx.StartCoroutine(IAttackResetRoutine());
        if (Ctx.AttackCount == 3)
        {
            Ctx.JumpCount = 0;
            Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
        }
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsAttackPressed && !Ctx.IsAttacking) {
            if (Ctx.IsDodgePressed) {
                SwitchState(Factory.Dodge());
            } else {
                SwitchState(Factory.Standard());
            }
        }
    }

    void HandleMotion()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Ctx.gameObject.transform.position, 2, Ctx.gameObject.transform.forward, out hit, 3, 1 << 15)) {
            Ctx.gameObject.transform.LookAt(hit.collider.gameObject.transform.position);
        }

        Ctx.AppliedMovementX = Mathf.Lerp(Ctx.AppliedMovementX, 0, 0.1f);
        Ctx.AppliedMovementZ = Mathf.Lerp(Ctx.AppliedMovementZ, 0, 0.1f);;
    }

    void HandleAttack()
    {
        if (Ctx.AttackCount < 3 && Ctx.CurrentAttackResetRoutine != null) {
            Ctx.StopCoroutine(Ctx.CurrentAttackResetRoutine);
        }

        // set variables
        Ctx.IsAttacking = true;
        Ctx.IsAttackPressed = false;
        Ctx.AttackCount += 1;

        // count attacks to transition to a specified animation
        Ctx.Animator.SetBool(Ctx.AttackingHash, true);
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, Ctx.AttackCount);
    }
}
