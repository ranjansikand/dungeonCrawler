using UnityEngine;

public class MobPatrol : MobBase
{
    float patrolRadius = 10f;
    int groundLayer = 1 << 7;

    public MobPatrol(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        Ctx.ReenableAgent();
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
        Vector3 patrolDestination = new Vector3( Ctx.Anchor.x + Random.Range(-patrolRadius, patrolRadius), 
                Ctx.transform.position.y, Ctx.Anchor.z + Random.Range(-patrolRadius, patrolRadius) 
            );

        if (Physics.Raycast(patrolDestination, -Ctx.transform.up, 2f, groundLayer)) {
            Ctx.Agent.SetDestination(patrolDestination);
        } else {
            HandleDestination();
        }
    }
}
