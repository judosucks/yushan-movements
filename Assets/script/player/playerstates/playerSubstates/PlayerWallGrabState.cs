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
        Debug.Log("holdposition"+stateMachine.CurrentState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       

        if (!isExitingState)
        {
            HoldPosition();
            
            
            if (normalInputY > 0)
            {
                Debug.Log("normalInpuy > 0 climb" + stateMachine.CurrentState);
               
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (normalInputY < 0 || !GrabInput)
            {
                Debug.Log("else if < 0 slide" + stateMachine.CurrentState);
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
        
    }
    private void HoldPosition()
    {
        Debug.Log("holdposition");
        player.transform.position = holdPosition;
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
        
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}
