using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJumpInAir : PlayerState
{
    private float xInput;

    private float yInput;

    private bool isGrounded;

    private bool runJumpInput;

    private bool coyoteTime;

    private bool isRunJumping;

    private bool RunJumpInputStop;

    private bool canRunJump;

    private bool isTouchingWall;

    private int normalInputX;

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
        runJumpInput = player.InputHandler.RunJumpInput;
        RunJumpInputStop = player.InputHandler.RunJumpInputStop;
        CheckRunJumpMultiplier();
        if (isGrounded && player.CurrentVelocity.y <0.01f )
        {
            
            stateMachine.ChangeState(player.RunJumpLandState);
            
        }else if(runJumpInput && canRunJump)
        {
            
            Debug.Log("going to run jump state from in air state");
            stateMachine.ChangeState(player.RunJumpState);
        }
        else if (isTouchingWall && normalInputX == player.facingDirection && player.CurrentVelocity.y <= 0f)
        {
            
            Debug.Log("chaning to wall slide state" + isTouchingWall + "" + xInput + "" + player.facingDirection);
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {

            player.SetVelocityX(playerData.inAirMovementForce * xInput);
            player.Anim.SetFloat("y", player.CurrentVelocity.y);
            player.Anim.SetFloat("x",Mathf.Abs(player.CurrentVelocity.x));
            Debug.Log("is not jump run grounded" + xInput+"xinput"+ player.CurrentVelocity.y+"isrunjumpstate"+stateMachine.CurrentState+normalInputX);
           
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

        Debug.Log("checkrunjumpmultiplier");
        if (isRunJumping)
        {
            if (RunJumpInputStop)
            {
                Debug.Log("checkrunjumpmultiplier is run jumping in run air state" + isRunJumping + "second jump");
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableRunJumpHeightMultiplier);
                isRunJumping = false;
            }


            else if (player.CurrentVelocity.y <= 0f)
            {
                isRunJumping = false;
            }
        }
    }
    public void SetIsRunJumping()
    {
        isRunJumping = true;
        Debug.Log("isrunjumping from runjumpinaire setisrunjumping" + isRunJumping+stateMachine.CurrentState);
    }
}
