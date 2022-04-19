using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isAnilityRunJumpDone;
    private bool isGrounded;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
        isAnilityRunJumpDone = false;
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
            if (isGrounded && Mathf.Sign(player.rb.velocity.y) <= 0f)
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
        if (isAnilityRunJumpDone)
        {
            if(isGrounded && Mathf.Sign(player.rb.velocity.y) <= 0f)
            {
                Debug.Log("onground run jump");
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                Debug.Log("going run jump in air state");
                stateMachine.ChangeState(player.RunJumpInAirState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
