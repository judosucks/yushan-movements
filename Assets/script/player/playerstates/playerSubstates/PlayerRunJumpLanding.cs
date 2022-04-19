using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJumpLanding : PlayerGroundedState
{
    

    public PlayerRunJumpLanding(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        xInput = (int)player.InputHandler.inputX;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(xInput != 0)
        {
            Debug.Log("xinput != 0 in run jump landing");
            stateMachine.ChangeState(player.MoveState);
        }
        else if(isAnimationFinished)
        {
            Debug.Log("animation finished for run jump landing going to idle");
            stateMachine.ChangeState(player.IdleState);
        }
    }

}
