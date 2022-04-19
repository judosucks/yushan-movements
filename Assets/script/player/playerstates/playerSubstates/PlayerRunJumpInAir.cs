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
        xInput = (int)player.InputHandler.inputX;
        yInput = (int)player.InputHandler.inputY;
        runJumpInput = player.InputHandler.RunJumpInput;
        runJumpInputStop = player.InputHandler.RunJumpInputStop;

        if(isGrounded && Mathf.Sign(player.CurrentVelocity.y) == 0f && xInput != 0)
        {
            Debug.Log("isgrounded going to runjumplandstate"+Mathf.Sign(player.CurrentVelocity.y));
            stateMachine.ChangeState(player.RunJumpLandState);
            Debug.Log("change state run jump state is excuted");
        }else if(runJumpInput && canRunJump)
        {
            Debug.Log("going to run jump state from in air state");
            stateMachine.ChangeState(player.RunJumpState);
        }
        else
        {
            player.MoveCharacter();
            player.Anim.SetFloat("y", player.CurrentVelocity.y);
            player.Anim.SetFloat("x",Mathf.Abs(player.CurrentVelocity.x));
            Debug.Log("is not jump run grounded"+Mathf.Sign(player.CurrentVelocity.y)+"anim"+player.Anim);
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
    public void SetIsRunJumping() => isRunJumping = true;
}
