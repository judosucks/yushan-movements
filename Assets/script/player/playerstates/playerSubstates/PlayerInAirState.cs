using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private float xInput;

    private bool isGrounded;

    private bool jumpInput;

    private bool coyoteTime;

    private bool isJumping;

    private bool jumpInputStop;

    private bool canJump;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckGrounded();
        canJump = player.JumpState.canJump();
        Debug.Log("docheck" + "isgrounded" + isGrounded);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckJumpMultiplier();
        CheckCoyoteTime();
        xInput = player.InputHandler.inputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        

        Debug.Log("isgrounded from inaire" + isGrounded+"xinput"+xInput);
        if(isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            Debug.Log("isgrounded" + isGrounded+"inairstate currentvelocityy"+player.CurrentVelocity.y);
            stateMachine.ChangeState(player.LandState);
        }else if (jumpInput && canJump)
        {
            Debug.Log("jumpinput canjump from in air state" + jumpInput + canJump);
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            
            
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity",Mathf.Abs( player.CurrentVelocity.x));
            Debug.Log("isnot grounded"+player.rb.velocity.x+""+player.CurrentVelocity.y);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            Debug.Log("cototetime" + playerData.coyoteTime);
            coyoteTime = false;
            player.JumpState.DeCreaseAmountOfJumpsLeft();
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            Debug.Log("checkjumpmultiplier isjumping from inairstate"+ isJumping);
            if (jumpInputStop)
            {
                Debug.Log("jumpinputstop from in air state" + jumpInputStop);
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
    public void SetIsJumping() => isJumping = true;
    
}
