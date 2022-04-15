using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private float xInput;

    private bool isGrounded;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();
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

        if(isGrounded && player.rb.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else
        {

            player.Run(1);
            player.Anim.SetFloat("yVelocity", player.rb.velocity.y);
            player.Anim.SetFloat("xVelocity",Mathf.Abs( player.rb.velocity.x));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
