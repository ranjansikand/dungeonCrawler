using System.Collections;
using UnityEngine;

public class MobAttackState : MobBaseState
{
    public MobAttackState(MobStateMachine currentContext, MobStateFactory enemyStateFactory)
     : base (currentContext, enemyStateFactory) {
        IsRootState = true;
    }

    public override void EnterState() {
        HandleAttack();
    }

    public override void UpdateState() {}

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsAttacking) {
            SwitchState(Factory.Chase());
        }
    }

    public override void InitializeSubState() {}

    void HandleAttack()
    {
        Ctx.IsAttacking = true;

        if (Vector3.Distance(Ctx.Target.position, Ctx.transform.position) > Ctx.AttackRadius) {
            Ctx.IsAttacking = false;
            return;
        }

        // for Animator.SetInt to control which attack to use
        int attackInt = Random.Range(1, (Ctx.NumberOfAttacks + 1));
        Ctx.Animator.SetInteger(Ctx.IsAttackingHash, attackInt);
    }
}
