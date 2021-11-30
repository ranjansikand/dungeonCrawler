using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPatrolState : MobBaseState
{
    private Collider target;
    const int layerMask = 1 << 10;
    static Collider[] targetsBuffer = new Collider[100];

    bool _patrolling = false;

    Vector3 destination;

    WaitForSeconds delay = new WaitForSeconds(0.5f);

    public MobPatrolState(MobStateMachine currentContext, MobStateFactory enemyStateFactory)
     : base (currentContext, enemyStateFactory) {
        IsRootState = true;
    }

    public override void EnterState() {
        Debug.Log("Entered patrol state");
        Ctx.Target = null;
        Ctx.PlayerDetected = false;

        Ctx.CurrentPatrolRoutine = Ctx.StartCoroutine(PatrolControl());
    }

    public override void UpdateState() {
        if (AcquireTarget()) {
            Ctx.PlayerDetected = true;
            Debug.Log("Player detected");
        }

        if (_patrolling && Vector3.Distance(destination, Ctx.transform.position) < 1) {
            _patrolling = false;
        }

        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.StopCoroutine(Ctx.CurrentPatrolRoutine);
        Ctx.Agent.SetDestination(Ctx.transform.position);

        Debug.Log("Exiting patrol state");
    }

    public override void CheckSwitchStates() {
        if (Ctx.PlayerDetected) {
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState() {}

    IEnumerator PatrolControl()
    {
        while (!Ctx.PlayerDetected) {
            yield return new WaitForSeconds(0.5f);

            if (!_patrolling) {
                _patrolling = true;
                Debug.Log("Patrolling");

                destination = Ctx.transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                Ctx.Agent.SetDestination(destination);
            }
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
