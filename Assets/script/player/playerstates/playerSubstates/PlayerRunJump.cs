using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJump : PlayerAbilityState
{
    private int amountOfRunJumpsLeft;

    public PlayerRunJump(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfRunJumpsLeft = playerData.amountOfRunJumps;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Jumping(player.CurrentVelocity.y);
        isAnilityRunJumpDone = true;
        amountOfRunJumpsLeft--;
        player.RunJumpInAirState.SetIsRunJumping();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public bool canRunJump()
    {
        if(amountOfRunJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetAmountOfRunJumpsLeft() => amountOfRunJumpsLeft = playerData.amountOfRunJumps;
    public void DecreaseAmountOfRunJumpsLeft() => amountOfRunJumpsLeft--;
}
