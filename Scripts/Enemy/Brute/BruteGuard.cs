// Guarding deflects incoming damage
// Substate of Detection

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteGuard : BruteBaseState
{
    bool _blocking;

    IEnumerator IBlockTime()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 4f));
        _blocking = false;
    }

    public BruteGuard(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        // Start animation
        Ctx.Animator.SetBool(Ctx.BlockingHash, true);

        _blocking = true;
        Ctx.IsBlocking = true;

        Ctx.StartCoroutine(IBlockTime());
    }

    public override void UpdateState() {
        CheckSwitchStates();

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
        if (!_blocking) {
            SwitchState(Factory.Attack());
        }
    }

    public override void InitializeSubState() {}
}
