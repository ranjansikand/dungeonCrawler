// Parent state for:
//      - Patrol, Looking
// Controls behavior states for undetected enemies

using UnityEngine;

public class MobSearching : MobBase
{
    static Collider[] targetsBuffer = new Collider[100];

    public MobSearching(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.Target = null;
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        CloseSubState();
    }

    public override void CheckSwitchStates() {
        if (Ctx.IsDead) {
            SwitchState(Factory.Dead());
        }
        else if (IsTargetVisible()) {
            SwitchState(Factory.Detected());
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
        if (AcquireTarget()) {
            Vector3 direction = Ctx.Target.position - Ctx.transform.position;

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
