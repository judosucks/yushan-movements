using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJumpInAir : PlayerState
{
    private float xInput;

    private bool isGrounded;

    private bool runJumpInput;

    private bool coyoteTime;

    public bool isRunJumping { get; private set; }

    private bool runJumpInputStop;

    private bool canRunJump;

    public PlayerRunJumpInAir(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckGrounded();
        canRunJump = player.RunJumpState.canRunJump();
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
        CheckRunJumpMultiplier();
        CheckCoyoteTime();
        xInput = player.InputHandler.inputX;
        runJumpInput = player.InputHandler.RunJumpInput;
        runJumpInputStop = player.InputHandler.RunJumpInputStop;

        if(isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.RunJumpLandState);
        }else if(runJumpInput && canRunJump)
        {
            stateMachine.ChangeState(player.RunJumpState);
        }
        else
        {
            player.MoveCharacter();
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity",Mathf.Abs(player.CurrentVelocity.x));
            Debug.Log("is not grounded");
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public void SetIsRunJumping()
    {

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
            player.Jumping(player.CurrentVelocity.y* playerData.variableRunJumpHeightMultiplier);
            isRunJumping = false;
        }
        else if(player.CurrentVelocity.y <= 0f)
        {
            isRunJumping = false;
        }
    }
    public void SetIsRunJump() => isRunJumping = true;
}
