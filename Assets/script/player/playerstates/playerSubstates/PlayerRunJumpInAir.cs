using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJumpInAir : PlayerState
{
    private int xInput;

    private int yInput;

    private bool isGrounded;

    private bool runJumpInput;

    private bool coyoteTime;

    public bool isRunJumping { get; private set; }

    public bool RunJumpInputStop { get; private set; }

    private bool canRunJump;

    private bool isTouchingWall;

    public bool isRunJumpState { get; private set; }

    public PlayerRunJumpInAir(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckGrounded();
        canRunJump = player.RunJumpState.canRunJump();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
        isRunJumpState = true;
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckRunJumpMultiplier();
        CheckCoyoteTime();
        xInput = (int)player.InputHandler.inputX;
        yInput = (int)player.InputHandler.inputY;
        runJumpInput = player.InputHandler.RunJumpInput;
        RunJumpInputStop = player.InputHandler.RunJumpInputStop;

        if(isGrounded && player.CurrentVelocity.y <0.01f && isRunJumpState)
        {
            isRunJumpState = false;
            stateMachine.ChangeState(player.RunJumpLandState);
            Debug.Log("change state run jump state is excuted" + isRunJumpState);
        }else if(runJumpInput && canRunJump)
        {
            isRunJumpState = false;
            Debug.Log("going to run jump state from in air state");
            stateMachine.ChangeState(player.RunJumpState);
        }
        else if (isTouchingWall && xInput == player.facingDirection && isRunJumpState)
        {
            isRunJumpState = false;
            Debug.Log("chaning to wall slide state" + isTouchingWall + "" + xInput + "" + player.facingDirection);
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            isRunJumpState = false;
            player.MoveCharacter();
            player.Anim.SetFloat("y", player.CurrentVelocity.y);
            player.Anim.SetFloat("x",Mathf.Abs(player.CurrentVelocity.x));
            Debug.Log("is not jump run grounded" + player.CurrentVelocity.y+"isrunjumpstate"+isRunJumpState);
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
            Debug.Log("coyototime" + playerData.coyoteTime);
            coyoteTime = false;
            player.RunJumpState.DecreaseAmountOfRunJumpsLeft();
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;

    private void CheckRunJumpMultiplier()
    {
        if (isRunJumping)
        {
            Debug.Log("checkrunjumpmultiplier is run jumping in run air state" + isRunJumping);
            player.SetVelocityY(player.CurrentVelocity.y * playerData.variableRunJumpHeightMultiplier);
            isRunJumping = false;
        }
        else if(player.CurrentVelocity.y <= 0f)
        {
            isRunJumping = false;
        }
    }
    public void SetIsRunJumping()
    {
        isRunJumping = true;
        Debug.Log("isrunjumping from runjumpinaire setisrunjumping" + isRunJumping);
    }
}
