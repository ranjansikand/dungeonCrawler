using UnityEngine;

public class MobAttack : MobBase
{
    public MobAttack(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        HandleAttack();

        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, 0);
    }

    public override void CheckSwitchStates() {
        if (!Ctx.Attacking) {
            SwitchState(Factory.Circle());
        }
    }

    public override void InitializeSubState() {}

    void HandleAttack()
    {
        Ctx.Attacking = true;

        int attack = 1;
        if (Ctx.NumberOfAttacks > 1) attack = Random.Range(1, Ctx.NumberOfAttacks + 1);
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, attack);
    }
}
