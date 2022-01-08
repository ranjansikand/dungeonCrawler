using System.Collections;
using UnityEngine;

public class MobChase : MobBase
{
    WaitForSeconds delay = new WaitForSeconds(0.5f);
    float strength = 1.5f;
    float timeInState, maxTimeInState;

    bool _targetInRange;

    public MobChase(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        _targetInRange = false;
        Ctx.ReenableAgent();
        Ctx.StartCoroutine(IHandlePursuit());

        maxTimeInState = 0.5f + Vector3.Distance(Ctx.Target.position, Ctx.transform.position)/Ctx.Agent.speed;
    }

    public override void UpdateState() {
        CheckSwitchStates();
        HandleRotation();

        // To prevent hangups
        timeInState += Time.deltaTime;
        if (timeInState > maxTimeInState) {
            _targetInRange = true;
        }
    }

    public override void ExitState() {
        Ctx.StopCoroutine(IHandlePursuit());
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (_targetInRange) {
            SwitchState(Factory.Attack());
        }
    }

    public override void InitializeSubState() {}

    IEnumerator IHandlePursuit()
    {
        while (!Ctx.Attacking) {
            yield return delay;

            if (Ctx.Target != null) {
                Ctx.Agent.SetDestination(Ctx.Target.position);

                if (Ctx.Agent.remainingDistance > Ctx.ChaseRange) {
                    // if player is too far away
                    Ctx.Target = null;
                }
                else if (Ctx.Agent.remainingDistance < Ctx.AttackRange) {
                    _targetInRange = true;
                }
            }
        }
    }

    void HandleRotation() {
        Quaternion targetRotation = Quaternion.LookRotation (Ctx.Target.position - Ctx.transform.position);
        float str = Mathf.Min (strength * Time.deltaTime, 1);
        Ctx.transform.rotation = Quaternion.Slerp (Ctx.transform.rotation, targetRotation, str);
    }
}
