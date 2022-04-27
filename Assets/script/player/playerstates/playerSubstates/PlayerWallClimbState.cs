using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerWallTouchingState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log(stateMachine.CurrentState);
        

        if (!isExitingState)
        {
            Debug.Log("wallclimb"+player.CurrentVelocity.y);
            player.SetVelocityY(playerData.wallClimbVelocity);
            if (normalInputY != 1)
            {
                Debug.Log("change to wall grab");
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
       
    }
}
