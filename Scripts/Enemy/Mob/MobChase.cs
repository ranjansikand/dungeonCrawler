using System.Collections;
using UnityEngine;

public class MobChase : MobBase
{
    WaitForSeconds delay = new WaitForSeconds(0.5f);

    Vector3 _playerRelativePos;
    float strength = 1.5f;

    bool _canAttack;

    public MobChase(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        _canAttack = false;
        Ctx.StartCoroutine(IHandlePursuit());
    }

    public override void UpdateState() {
        CheckSwitchStates();
        HandleRotation();
    }

    public override void ExitState() {
        Ctx.StopCoroutine(IHandlePursuit());
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (_canAttack) {
            SwitchState(Factory.Attack());
        }
    }

    public override void InitializeSubState() {}

    IEnumerator IHandlePursuit()
    {
        while (true) {
            yield return delay;

            if (Ctx.Target != null) {
                _playerRelativePos = Ctx.transform.InverseTransformPoint(Ctx.Target.position);

                if (_playerRelativePos.magnitude > Ctx.ChaseRange) {
                    // if player is too far away
                    Ctx.Agent.SetDestination(Ctx.transform.position);
                    Ctx.Target = null;
                }
                else if (_playerRelativePos.magnitude < Ctx.AttackRange) {
                    _canAttack = true;
                }
                else {
                    Ctx.Agent.SetDestination(Ctx.Target.position);
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
