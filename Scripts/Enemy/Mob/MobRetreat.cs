using System.Collections;
using UnityEngine;

public class MobRetreat : MobBase
{
    Vector3 _destination = new Vector3(-30, -30, -30);
    Collider[] _results = new Collider[10];
    WaitForSeconds _backupDelay = new WaitForSeconds(0.5f);

    public MobRetreat(MobMachine currentContext, MobFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void EnterState() {
        CheckForBackup();
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {}

    public override void CheckSwitchStates() {
        if (Vector3.Distance(Ctx.transform.position, _destination) < 1) {
            SwitchState(Factory.Chase());
        }
    }

    public override void InitializeSubState() {}

    void CheckForBackup()
    {
        int hits = Physics.OverlapSphereNonAlloc(Ctx.transform.position, Ctx.SightRange, _results, 1 << 15);

        if (hits > 0) {

            for (int i = 0; i < hits; i++) {
                if (_results[i].gameObject.GetComponent<MobMachine>().Target == null) {
                    Debug.Log("Calling " + _results[i].gameObject.name);
                    AssignTarget(_results[i].gameObject.GetComponent<MobMachine>().Target);

                    Ctx.Animator.SetTrigger("callForHelp");
                }
            }
        } else if (Random.Range(1, 10) == 2) {
            Ctx.Agent.SetDestination((Ctx.transform.position - Ctx.Target.position).normalized * 15);
        }
    }

    IEnumerator AssignTarget(Transform target)
    {
        // calls all enemies that have not seen the player yet
        yield return _backupDelay;
        target = Ctx.Target;
    }
}
