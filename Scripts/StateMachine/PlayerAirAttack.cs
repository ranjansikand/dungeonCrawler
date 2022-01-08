using System.Collections;
using UnityEngine;

public class PlayerAirAttack : PlayerBaseState
{
    bool _started;

    public PlayerAirAttack(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        InitializeSubState();
    }

    public override void EnterState() {
        _started = false;
        Debug.Log("Air attack initiated");
    }

    public override void UpdateState() {
        if (!_started) {
            if (Physics.Raycast(Ctx.transform.position, Vector3.down, 3, 7)) {
                _started = true;
                HandleAttack();
            }
        }
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.AttackingHash, false);
        Ctx.AttackCount = 1;
        Ctx.IsAttackPressed = false;
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsAttacking && Ctx.IsGrounded) {
            if (Ctx.IsDodgePressed) {
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

    void HandleAttack()
    {
        Ctx.AttackCount = 4;

        Ctx.Animator.SetBool(Ctx.AttackingHash, true);
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, Ctx.AttackCount);
    }

}
