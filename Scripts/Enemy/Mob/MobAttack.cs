using System.Collections;
using UnityEngine;

public class MobAttack : MobBase
{
    IEnumerator IAutoReset()
    {
        yield return new WaitForSeconds(2f);

        if (Ctx.Attacking) Ctx.Attacking = false;
    }
    public MobAttack(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        Ctx.StartCoroutine(IAutoReset());
        HandleAttack();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.StopCoroutine(IAutoReset());
    }

    public override void CheckSwitchStates() {
        if (!Ctx.Attacking) {
            SwitchState(Factory.Chase());
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
