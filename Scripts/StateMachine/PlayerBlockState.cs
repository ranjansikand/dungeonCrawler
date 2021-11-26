// Block makes it so the player is un-injurable for a period of time
using System.Collections;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    IEnumerator IBlockReset()
    {
        yield return Ctx.BlockResetDelay;

        Ctx.BlockTimeRemaining = Ctx.MaxBlockTime;
    }

    public PlayerBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        CheckBlock();
    }

    public override void UpdateState() {
        if (Ctx.BlockTimeRemaining <= 0) Ctx.IsBlocking = false;
        else Ctx.BlockTimeRemaining -= Time.deltaTime;

        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.IsBlocking = false;
        Ctx.StartCoroutine(IBlockReset());
        Ctx.OnBlockEnded.Invoke();
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (!Ctx.IsBlockPressed || !Ctx.IsBlocking) {
            SwitchState(Factory.Standard());
        }
    }

    void CheckBlock()
    {
        if (Ctx.BlockTimeRemaining == Ctx.MaxBlockTime) {
            Ctx.IsBlocking = true;
            Ctx.OnBlockStarted.Invoke();
        } else {
            Ctx.IsBlocking = false;
            Ctx.IsBlockPressed = false;
        }
    }
}
