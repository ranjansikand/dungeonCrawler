using System.Collections;
using UnityEngine;

public class MobLooking : MobBase
{
    bool _readyToMove = false;

    public MobLooking(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsLeafState = true;
    }

    public override void EnterState() {
        Ctx.StartCoroutine(ILookingRoutine());
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (_readyToMove) {
            SwitchState(Factory.Patrol());
        }
    }

    public override void InitializeSubState() {}

    IEnumerator ILookingRoutine() {
        yield return new WaitForSeconds(Random.Range(0f, 5f));

        _readyToMove = true;
    }
}
