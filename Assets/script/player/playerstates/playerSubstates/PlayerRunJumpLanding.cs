using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJumpLanding : PlayerGroundedState
{
    private int normalInputX;

    public PlayerRunJumpLanding(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        normalInputX = player.InputHandler.normalInputX;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Debug.Log("runjumplanding" + isExitingState);
            if (normalInputX != 0)
            {
                Debug.Log("xinput != 0 in run jump landing chamge to move"+stateMachine.CurrentState);
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                Debug.Log("animation finished for run jump landing going to idle" + stateMachine.CurrentState);
                stateMachine.ChangeState(player.IdleState);
            }
        }
        
    }

}
