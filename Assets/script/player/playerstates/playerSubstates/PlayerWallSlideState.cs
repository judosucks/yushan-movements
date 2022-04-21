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
        player.SetVelocityY(-playerData.wallSlideVelocity);

        if(GrabInput && normalInputY == 0 && !isExitingState)
        {
            Debug.Log("change to grabstate" + stateMachine.CurrentState);
            stateMachine.ChangeState(player.WallGrabState);
        }
    }
}
