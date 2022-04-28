using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJumpInAir : PlayerState
{
    private float xInput;

    private float yInput;

    private bool isGrounded;

    private bool runJumpInput;

    private bool runJumpCoyoteTime;

    private bool isRunJumping;

    private bool RunJumpInputStop;

    private bool canRunJump;

    private bool isTouchingWall;

    private int normalInputX;

    private bool GrabInput;

    private bool isTouchingWallBack;

    private bool wallRunJumpCoyoteTime;

    private float startRunJumpWallJumpCoyoteTime;

    private bool oldIsRunJumpTouchingWall;

    private bool oldIsRunJumpTouchingWallBack;

    public PlayerRunJumpInAir(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        oldIsRunJumpTouchingWall = isTouchingWall;
        oldIsRunJumpTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckGrounded();
        canRunJump = player.RunJumpState.canRunJump();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        if(!wallRunJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsRunJumpTouchingWall|| oldIsRunJumpTouchingWallBack))
        {
            StartRunJumpWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
      
    }

    public override void Exit()
    {
        base.Exit();
        player.SetGravityScale(playerData.gravityScale);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckRunJumpCoyoteTime();
        CheckRunJumpWallJumpCoyoteTime();
        xInput = player.InputHandler.inputX;
        yInput = player.InputHandler.inputY;
        normalInputX = player.InputHandler.normalInputX;
        runJumpInput = player.InputHandler.RunJumpInput;
        RunJumpInputStop = player.InputHandler.RunJumpInputStop;
        GrabInput = player.InputHandler.GrabInput;
        CheckRunJumpMultiplier();
        if (isGrounded && player.CurrentVelocity.y <0.01f )
        {
            Debug.Log("isgrounded" + stateMachine.CurrentState);
            stateMachine.ChangeState(player.RunJumpLandState);
            
        }
        else if(runJumpInput && (isTouchingWall||isTouchingWallBack || runJumpCoyoteTime))
        {
            Debug.Log("walljumpinput istouchingwall back istouchingwall" + stateMachine.CurrentState);
            StopRunJumpWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
             stateMachine.ChangeState(player.WallJumpState);
        }
        else if(runJumpInput && canRunJump)
        {
            
            Debug.Log("going to run jump state from in air state");
            
            stateMachine.ChangeState(player.RunJumpState);
        }else if(isTouchingWall && GrabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && normalInputX == player.facingDirection && player.CurrentVelocity.y <= 0f)
        {
            
            Debug.Log("chaning to wall slide state" + isTouchingWall + "" + xInput + "" + player.facingDirection);
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (player.rb.velocity.y < 0)
        {
            //quick fall when holding down: feels responsive, adds some bonus depth with very little added complexity and great for speedrunners :D (In games such as Celeste and Katana ZERO)
            if (player.InputHandler.normalInputY < 0)
            {
                player.SetGravityScale(playerData.gravityScale * playerData.quickFallGravityMultiplier);
            }
            else
            {
                player.SetGravityScale(playerData.gravityScale * playerData.fallGravityMultiplier);
            }
        }
        else
        {
           
            //player.SetVelocityX(playerData.inAirMovementForce * xInput);
            //player.Anim.SetFloat("y", player.CurrentVelocity.y);
            //player.Anim.SetFloat("x",Mathf.Abs(player.CurrentVelocity.x));
            //Debug.Log("is in air" + player.CurrentVelocity.x+"xinput"+ player.CurrentVelocity.y+"isrunjumpstate"+stateMachine.CurrentState+normalInputX);
           
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.Drag(playerData.dragAmount);
        player.Run(1);
    }
    
    private void CheckRunJumpCoyoteTime()
    {
        if(runJumpCoyoteTime && Time.time > runJumpStartTime + playerData.coyoteTime)
        {
            Debug.Log("coyototime" + playerData.runJumpCoyoteTime);
            runJumpCoyoteTime = false;
            player.RunJumpState.DecreaseAmountOfRunJumpsLeft();
        }
    }
   

    private void CheckRunJumpMultiplier()
    {

        Debug.Log("checkrunjumpmultiplier");
        if (isRunJumping)
        {
            if (RunJumpInputStop)
            {
                Debug.Log("checkrunjumpmultiplier is run jumping in run air state" + isRunJumping + "second jump");
                //player.RunJumping(player.CurrentVelocity.y * playerData.variableRunJumpHeightMultiplier);
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableRunJumpHeightMultiplier);
                isRunJumping = false;
            }


            else if (player.CurrentVelocity.y <= 0f)
            {
                isRunJumping = false;
            }
        }
    }

    private void CheckRunJumpWallJumpCoyoteTime()
    {
        if(wallRunJumpCoyoteTime && Time.time > startRunJumpWallJumpCoyoteTime + playerData.runJumpCoyoteTime)
        {
            wallRunJumpCoyoteTime = false;
        }
    }

    public void StartRunJumpCoyoteTime() => runJumpCoyoteTime = true;

    public void StartRunJumpWallJumpCoyoteTime()
    {
        wallRunJumpCoyoteTime = true;
        startRunJumpWallJumpCoyoteTime = Time.time;

    }
    public void StopRunJumpWallJumpCoyoteTime() => wallRunJumpCoyoteTime = false;

    public void SetIsRunJumping()
    {
        isRunJumping = true;
        Debug.Log("isrunjumping from runjumpinaire setisrunjumping" + isRunJumping+stateMachine.CurrentState);
    }
}
