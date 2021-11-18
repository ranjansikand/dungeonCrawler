using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    Vector3 patrolCenter, destination;

    WaitForSeconds delay = new WaitForSeconds(8);

    // detection
    private Collider target;
    const int layerMask = 1 << 10;
    static Collider[] targetsBuffer = new Collider[100];

    public EnemyPatrolState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
        : base (currentContext, enemyStateFactory) {
            IsRootState = true;
            InitializeSubState();
        }

    public override void EnterState() {
        Ctx.PlayerDetected = false;
        Ctx.Target = null;

        // base patrol around starting place
        patrolCenter = Ctx.transform.position;

        Ctx.CurrentPatrolRoutine = Ctx.StartCoroutine(PatrolArea());
    }

    public override void UpdateState() {
        // look for player
        if (AcquireTarget()) {
            Ctx.PlayerDetected = true;
        }

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        if (Ctx.CurrentPatrolRoutine != null) Ctx.StopCoroutine(Ctx.CurrentPatrolRoutine);
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (Ctx.PlayerDetected) {
            SwitchState(Factory.Detected());
        }
    }

    public override void InitializeSubState() {
        SetSubState(Factory.Standard());
    }

    IEnumerator PatrolArea()
    {
        while (!Ctx.IsDead) {
            destination = patrolCenter + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            Ctx.Agent.SetDestination(destination);

            yield return delay;
        }
    }

    bool AcquireTarget () {
        Vector3 a = Ctx.transform.localPosition;
        Vector3 b = a;
        b.y += 2f;
        int hits = Physics.OverlapCapsuleNonAlloc(
            a, b, Ctx.SightRange, targetsBuffer, layerMask);
        if (hits > 0) {
            for (int i = 0; i < hits; i++)
            {
                target = targetsBuffer[i].GetComponent<Collider>();
                Ctx.Target = target.gameObject.transform;
            }
            Debug.Assert(target != null, "Targeted non-enemy!", targetsBuffer[0]);
            return true;
        }
        target = null;
        return false;
    }
}
