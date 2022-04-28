using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState


{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("walljumpstate");
        player.RunJumpState.ResetAmountOfRunJumpsLeft();
        player.JumpState.ResetAmountOfJumpsLeft();
        wallJumpDirection = player.facingDirection;
        player.WallJump(wallJumpDirection);
        player.JumpState.DeCreaseAmountOfJumpsLeft();
        player.RunJumpState.DecreaseAmountOfRunJumpsLeft();
        Debug.Log("isfacingright"+player.IsFacingRight+"sss"+wallJumpDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        //player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        //if(Time.time >= startTime * playerData.wallJumpTime)
        //{
        //    isAbilityDone = true;
        //}
        player.Anim.SetFloat("y", player.CurrentVelocity.y);
        player.Anim.SetFloat("x", Mathf.Abs(player.CurrentVelocity.x));

        if (Time.time >= runJumpStartTime * playerData.runJumpWallJumpTime)
        {
            //isAbilityDone = true;
            isAbilityRunJumpDone = true;
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.Drag(playerData.dragAmount);
        player.Run(playerData.wallJumpRunLerp);
    }
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            Debug.Log("from wall jump state"+ wallJumpDirection);
            wallJumpDirection = player.facingDirection;
        }
        else
        {
            Debug.Log("from wall jump state" + wallJumpDirection);
            wallJumpDirection = -player.facingDirection;
        }
    }
}
