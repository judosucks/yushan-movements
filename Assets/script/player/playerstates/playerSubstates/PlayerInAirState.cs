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

        CheckCoyoteTime();
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
        else if (jumpInput && isTouchingWall)
        {
            Debug.Log("walljumpinput" + stateMachine.CurrentState); stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && canJump)
        {
            Debug.Log("jumpinput canjump from in air state" + jumpInput + canJump);
            player.InputHandler.UseJumpInput();
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
        else
        {

                     player.SetVelocityX(playerData.inAirMovementForce * xInput);
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

            Debug.Log("else in air state" + stateMachine.CurrentState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
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
    public void StartCoyoteTime() => coyoteTime = true;

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
