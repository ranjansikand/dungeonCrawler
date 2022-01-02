using UnityEngine;

public class MobAttack : MobBase
{
    float strength = 0.5f;
    float _enterTime;

    public MobAttack(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        _enterTime = Time.time;
        HandleAttack();

        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void UpdateState() {
        HandleRotation();
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, 0);
    }

    public override void CheckSwitchStates() {
        if (!Ctx.Attacking || Time.time > _enterTime + 3.5f) {
            SwitchState(Factory.Waiting());
        }
    }

    public override void InitializeSubState() {}

    void HandleAttack()
    {
        Ctx.Attacking = true;

        // Start attack animation
        int attack = 1;
        if (Ctx.NumberOfAttacks > 1) attack = Random.Range(1, Ctx.NumberOfAttacks + 1);
        Ctx.Animator.SetInteger(Ctx.AttackCountHash, attack);
    }

    void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation (Ctx.Target.position - Ctx.transform.position);
        float str = Mathf.Min (strength * Time.deltaTime, 1);
        Ctx.transform.rotation = Quaternion.Lerp (Ctx.transform.rotation, targetRotation, str);
    }
}
