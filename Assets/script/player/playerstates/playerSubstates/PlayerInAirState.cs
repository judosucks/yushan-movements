using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private float xInput;

    private bool isGrounded;

    private bool jumpInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();
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

        xInput = player.InputHandler.inputX;
        jumpInput = player.InputHandler.JumpInput;

        Debug.Log("isgrounded from inaire" + isGrounded+"xinput"+xInput);
        if(isGrounded && player.rb.velocity.y < 0.01f)
        {
            Debug.Log("isgrounded" + isGrounded+"inairstate currentvelocityy"+player.CurrentVelocity.y);
            stateMachine.ChangeState(player.LandState);
        }else if (jumpInput && player.JumpState.canJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            
            player.Run(1);
            player.Anim.SetFloat("yVelocity", player.rb.velocity.y);
            player.Anim.SetFloat("xVelocity",Mathf.Abs( player.rb.velocity.x));
            Debug.Log("isnot grounded"+player.rb.velocity.x+""+player.CurrentVelocity.y);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
