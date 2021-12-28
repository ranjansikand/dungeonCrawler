using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteSearching : BruteBaseState
{
    static Collider[] targetsBuffer = new Collider[100];

    public BruteSearching(BruteMachine currentContext, BruteStateFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.Status.UpdateSprite(Ctx.Confused);
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (IsTargetVisible()) {
            SwitchState(Factory.Detected());
        } else if (Ctx.IsDead) {
            SwitchState(Factory.Dead());
        }
    }

    public override void InitializeSubState() {
        SetSubState(Factory.Patrol());
    }

    bool AcquireTarget () {
        Vector3 a = Ctx.transform.localPosition;
        Vector3 b = a;
        b.y += 2f;
        int hits = Physics.OverlapCapsuleNonAlloc(
            a, b, Ctx.SightRange, targetsBuffer, Ctx.LayerMask);
        if (hits > 0) {
            for (int i = 0; i < hits; i++)
            {
                Ctx.Target = targetsBuffer[i].GetComponent<Collider>().gameObject.transform;
            }
            Debug.Assert(Ctx.Target != null, "Targeted non-enemy!", targetsBuffer[0]);
            return true;
        }
        Ctx.Target = null;
        return false;
    }

    bool IsTargetVisible()
    {
        // Raycasts to target to simulate vision
        if (AcquireTarget()) {
            Vector3 direction = Ctx.Target.position - Ctx.transform.position;

            // Checks if any targets are between self and target
            if (!Physics.Raycast(Ctx.transform.position + Vector3.up, direction, (0.9f * direction.magnitude))) {
                return true;
            }
            else {
                Ctx.Target = null;
            }
        }

        return false;
    }
}
