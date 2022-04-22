using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerWallTouchingState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (!isExitingState)
        {
            player.SetVelocityY(-playerData.wallSlideVelocity);
            Debug.Log("wallslide" + player.CurrentVelocity.y);
            if (GrabInput && normalInputY == 0)
            {
                Debug.Log("change to grabstate" + stateMachine.CurrentState);
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
        
    }
}
