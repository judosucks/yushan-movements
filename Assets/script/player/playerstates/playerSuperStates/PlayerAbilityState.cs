using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    private bool isGrounded;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (isGrounded && player.rb.velocity.y < 0.01f)
            {
                Debug.Log("on ground"+isGrounded);
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                Debug.Log("inairstate"+player.CurrentVelocity.y);
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
