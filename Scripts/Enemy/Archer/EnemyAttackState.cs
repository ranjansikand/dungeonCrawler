using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
        : base (currentContext, enemyStateFactory) {
            IsLeafState = true;
        }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, true);
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, false);
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.PlayerDetected) {
            SwitchState(Factory.Standard());
        }
    }
}
