using System.Collections;
using UnityEngine;

public class MobCircling : MobBase
{
    Vector3 randomPosition;

    public MobCircling(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        Debug.Log("Here");
        HandleDestination();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (Vector3.Distance(Ctx.transform.position, randomPosition) < 1) {
            Debug.Log("At location");
            SwitchState(Factory.Chase());
        }
    }

    public override void InitializeSubState() {}

    void HandleDestination()
    {
        randomPosition = Ctx.Target.position + 
            new Vector3(Random.Range(-Ctx.AttackRange, Ctx.AttackRange), 
            0, 
            Random.Range(-Ctx.AttackRange, Ctx.AttackRange));
        
        randomPosition = Vector3.zero;

        Ctx.Agent.SetDestination(randomPosition);
    }
}
