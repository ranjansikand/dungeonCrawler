using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteLooking : BruteBaseState
{
    bool _canMove;

    public BruteLooking(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        _canMove = false;
        // Set animation
        Ctx.StartCoroutine(IReturnToPatrol());
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        // Stop animation
    }

    public override void CheckSwitchStates() {
        if (_canMove) {
            SwitchState(Factory.Patrol());
        }
    }

    public override void InitializeSubState() {}

    IEnumerator IReturnToPatrol() {
        yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        _canMove = true;
    }
}
