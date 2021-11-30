// Parent state for:
//      - Patrol, Looking
// Controls behavior states for undetected enemies

using UnityEngine;

public class MobSearching : MobBase
{
    static Collider[] targetsBuffer = new Collider[100];

    // Fields of view
    float _viewAngle = 100f;

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
        else if (AcquireTarget()) {
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

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(Ctx.transform.position, Ctx.SightRange, Ctx.LayerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - Ctx.Eyes.position).normalized;

            if (Vector3.Angle(Ctx.transform.forward, dirToTarget) < _viewAngle/2) {
                float distToTarget = Vector3.Distance(Ctx.Eyes.position, target.position);

                if (!Physics.Raycast(Ctx.Eyes.position, dirToTarget, distToTarget, 1 << 6)) {
                    Ctx.Target = target;
                    return;
                }
            }
        }

        Ctx.Target = null;
    }
}
