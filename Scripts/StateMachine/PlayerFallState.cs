using System.Collections;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    WaitForSeconds _delay = new WaitForSeconds(0.25f);

    IEnumerator IFallToDeath()
    {
        yield return new WaitForSeconds(1);

        int fallDamage = 0;
        while (!Ctx.IsGrounded) {
            yield return _delay;
            fallDamage += 1;
        }
        Ctx.GetComponent<Combat>().Damage(fallDamage);
    }

    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.IsGrounded = false;
        Ctx.Animator.SetBool(Ctx.IsFallingHash, true);
        Ctx.StartCoroutine(IFallToDeath());
    }

    public override void UpdateState() {
        CheckSwitchStates();
        HandleGravity();
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.IsFallingHash, false);
        Ctx.StopCoroutine(IFallToDeath());
    }

    public override void InitializeSubState() {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Walk());
        } else {
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates() {
        if (Ctx.IsGrounded) {
            SwitchState(Factory.Grounded());
        }
    }

    void HandleGravity()
    {
        bool isFalling = Ctx.CurrentMovementY <= 0.05f || !Ctx.IsJumpPressed;
        float fallMultiplier = 2.0f;

        if (isFalling)
        {
            float previousYVelocity = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.JumpGravities[Ctx.JumpCount] * fallMultiplier * Time.deltaTime);
            Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, -20.0f);
        }
        else 
        {
            float previousYVelocity = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.JumpGravities[Ctx.JumpCount] * Time.deltaTime);
            Ctx.AppliedMovementY = (previousYVelocity +Ctx.CurrentMovementY) * 0.5f;
        }
    }
}
