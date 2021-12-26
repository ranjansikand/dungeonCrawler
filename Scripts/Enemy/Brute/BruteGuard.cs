// Guarding deflects incoming damage
// Substate of Detection

using System.Collections;
using UnityEngine;

public class BruteGuard : BruteBaseState
{
    IEnumerator IBlockTime() {
        yield return new WaitForSeconds(Random.Range(2.5f, 6f));
        CheckSwitchStates();
    }

    public BruteGuard(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        // Start animation
        Ctx.Animator.SetBool(Ctx.BlockingHash, true);
        Ctx.IsBlocking = true;

        Ctx.StartCoroutine(IBlockTime());
    }

    public override void UpdateState() {
        if (Ctx.DistanceToTarget > Ctx.AttackRange)
        {
            Ctx.Agent.SetDestination(Ctx.Target.position);
        }
        else {
            Ctx.Agent.SetDestination(Ctx.transform.position);
        }
    }

    public override void ExitState() {
        // Stop Animation
        Ctx.Animator.SetBool(Ctx.BlockingHash, false);
        Ctx.IsBlocking = false;
        Ctx.StopCoroutine(IBlockTime());
    }

    public override void CheckSwitchStates() {
        if (Ctx.DistanceToTarget < Ctx.AttackRange) {
            SwitchState(Factory.Attack());
        }
        else {
            SwitchState(Factory.Pursue());
        }
    }

    public override void InitializeSubState() {}
}
