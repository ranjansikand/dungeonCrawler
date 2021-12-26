using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAttack : BruteBaseState
{
    public BruteAttack(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        Ctx.IsAttacking = true;

        // Start animation
        Ctx.Animator.SetInteger(Ctx.AttackIntHash, Random.Range(0, Ctx.NumberOfAttacks));
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        // Reset attack int
        Ctx.Animator.SetInteger(Ctx.AttackIntHash, -1);
    }

    public override void CheckSwitchStates() {
        if (!Ctx.IsAttacking) {
            SwitchState(Factory.Block());
        }
    }

    public override void InitializeSubState() {}
}
