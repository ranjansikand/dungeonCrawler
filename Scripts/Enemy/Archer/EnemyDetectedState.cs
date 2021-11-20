using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectedState : EnemyBaseState
{
    float _rotationFactorPerFrame = 15.0f;

    public EnemyDetectedState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
        : base (currentContext, enemyStateFactory) {
            IsRootState = true;
            InitializeSubState();
        }

    public override void EnterState() {}

    public override void UpdateState() {
        // look for player
        if (PlayerEscaped()) Ctx.PlayerDetected = false;
        else RotateToFace();

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        if (Ctx.CurrentPatrolRoutine != null) Ctx.StopCoroutine(Ctx.CurrentPatrolRoutine);
        Ctx.Agent.SetDestination(Ctx.transform.position);
    }

    public override void CheckSwitchStates() {
        if (!Ctx.PlayerDetected) {
            SwitchState(Factory.Patrol());
        }
    }

    public override void InitializeSubState() {
        SetSubState(Factory.Attack());
    }

    bool PlayerEscaped()
    {
        if (Vector3.Distance(Ctx.transform.position, Ctx.Target.position) > Ctx.SightRange * 1.5) return true;
        else return false;
    }

    void RotateToFace()
    {
        Vector3 targetDirection = Ctx.Target.position - Ctx.transform.position;
        Quaternion currentRotation = Ctx.transform.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Ctx.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
    }
}
