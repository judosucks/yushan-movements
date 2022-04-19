using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;

    private int yInput;

    private bool isGrounded;

    private bool jumpInput;

    private bool coyoteTime;

    private bool isJumping;

    private bool jumpInputStop;

    private bool canJump;

    private bool isTouchingWall;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
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
        xInput = (int)player.InputHandler.inputX;
        yInput = (int)player.InputHandler.inputY;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        

        Debug.Log("isgrounded from inaire" + isGrounded+"xinput"+xInput);
        if(isGrounded && Mathf.Sign(player.CurrentVelocity.y) == 0f && xInput == 0)
        {
            Debug.Log("isgrounded" + isGrounded+"inairstate currentvelocityy"+Mathf.Sign(player.CurrentVelocity.y));
            stateMachine.ChangeState(player.LandState);
            Debug.Log("state change land state excuted from in air state");
        }else if (jumpInput && canJump)
        {
            Debug.Log("jumpinput canjump from in air state" + jumpInput + canJump);
            stateMachine.ChangeState(player.JumpState);
        }else if(isTouchingWall && xInput == player.facingDirection)
        {
            Debug.Log("chaning to wall slide state"+ isTouchingWall+""+xInput+""+player.facingDirection);
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {

            player.MoveCharacter();
            player.Anim.SetFloat("yVelocity", player.rb.velocity.y);
            player.Anim.SetFloat("xVelocity",Mathf.Abs( player.rb.velocity.x));
            Debug.Log("is not jump run grounded" + Mathf.Sign(player.CurrentVelocity.y) + "anim" + player.Anim);
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
