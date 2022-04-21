using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerWallTouchingState
{
    private Vector2 holdPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;
        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldPosition();

        if (!isExitingState)
        {
            if (normalInputY > 0)
            {
                Debug.Log("normalInpuy > 0" + stateMachine.CurrentState);
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (normalInputY < 0 || GrabInput)
            {
                Debug.Log("else if < 0" + stateMachine.CurrentState);
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
        
    }
    private void HoldPosition()
    {
        Debug.Log("holdposition");
        player.transform.position = holdPosition;
        player.rb.velocity = Vector2.zero;
        
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
