using UnityEngine;
using UnityEngine.AI;

public class MobPatrol : MobBase
{
    float patrolRadius = 10f;
    int groundLayer = 1 << 7;

    public MobPatrol(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        HandleDestination();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.Agent.ResetPath();
    }

    public override void CheckSwitchStates() {
        if (Ctx.Agent.remainingDistance < 1f) {
            SwitchState(Factory.Looking());
        }
    }

    public override void InitializeSubState() {}

    void HandleDestination()
    {
        Vector3 patrolDestination = new Vector3(
                Ctx.transform.position.x + Random.Range(-patrolRadius, patrolRadius), 
                Ctx.transform.position.y, 
                Ctx.transform.position.z + Random.Range(-patrolRadius, patrolRadius) 
            );

        if (Physics.Raycast(patrolDestination, -Ctx.transform.up, 2f, groundLayer)) {
            Ctx.Agent.SetDestination(patrolDestination);
        } else {
            HandleDestination();
        }
    }
}
