using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private float xInput;

    private float yInput;

    private bool isGrounded;

    private bool jumpInput;

    private bool coyoteTime;

    private bool isJumping;

    private bool jumpInputStop;

    private bool canJump;

    private bool isTouchingWall;

    private int normalInputX;

    private bool GrabInput;

    private bool isTouchingWallBack;

    private bool wallJumpCoyoteTime;

    private float startWallJumpCoyoteTime;

    private bool oldIsTouchingWall;

    private bool oldIsTouchingWallBack;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        canJump = player.JumpState.canJump();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
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

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        xInput = player.InputHandler.inputX;
        yInput = player.InputHandler.inputY;
        normalInputX = player.InputHandler.normalInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        GrabInput = player.InputHandler.GrabInput;
        CheckJumpMultiplier();

        Debug.Log("isgrounded from inaire" + isGrounded + "xinput" + xInput+"normal"+normalInputX);
        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            Debug.Log("isgrounded" + isGrounded + "inairstate currentvelocityy" + player.CurrentVelocity.y);

            stateMachine.ChangeState(player.LandState);

            Debug.Log("state change land state excuted from in air state");
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            Debug.Log("walljumpinput" + stateMachine.CurrentState);
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            Debug.Log("playerwalljumpstate.determinewalljumpdirection" + isTouchingWall);
            player.InputHandler.UseGrabInput();            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && canJump)
        {
            Debug.Log("jumpinput canjump from in air state" + jumpInput + canJump);
           
            stateMachine.ChangeState(player.JumpState);

        }
        else if (isTouchingWall && GrabInput)
        {
            Debug.Log("grabwall from air state" + stateMachine.CurrentState);
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && normalInputX == player.facingDirection && player.CurrentVelocity.y <= 0f)
        {
            Debug.Log("chaning to wall slide state" + isTouchingWall + "" + xInput + "" + player.facingDirection + "normalx" + normalInputX);
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (player.rb.velocity.y < 0)
        {
            Debug.Log("if p rb v y < 0");
            //quick fall when holding down: feels responsive, adds some bonus depth with very little added complexity and great for speedrunners :D (In games such as Celeste and Katana ZERO)
            if (player.InputHandler.normalInputY < 0)
            {
                Debug.Log("p in nory < 0");
                player.SetGravityScale(playerData.gravityScale * playerData.quickFallGravityMultiplier);
            }
            else
            {
                Debug.Log("else setgravityscale");
                player.SetGravityScale(playerData.gravityScale * playerData.fallGravityMultiplier);
            }
        }
        else
        {
            player.Drag(playerData.dragAmount);
            player.Run(1);

            //         player.SetVelocityX(playerData.inAirMovementForce * xInput);
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

            Debug.Log("else in air state" + stateMachine.CurrentState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //player.Drag(playerData.dragAmount);
        //player.Run(1);
    }
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            Debug.Log("cototetime" + playerData.coyoteTime);
            coyoteTime = false;
            player.JumpState.DeCreaseAmountOfJumpsLeft();
        }
    }
    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    private void CheckJumpMultiplier()
    {
        Debug.Log("checkjumpmultiplier is excuted");
        if (isJumping)
        {
            Debug.Log("isjumping" + isJumping);
            if (jumpInputStop)
            {
                Debug.Log("jumpinputstop");
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                Debug.Log("jumpinputstop from in air state" + jumpInputStop + "second jump");
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
    public void SetIsJumping()
    {
        Debug.Log("setisjumping" + stateMachine.CurrentState);
        isJumping = true;

     }
    
}
