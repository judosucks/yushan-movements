using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isAbilityRunJumpDone;
    private bool isGrounded;
    private string AnimBoolName;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        AnimBoolName = animBoolName;
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
        isAbilityRunJumpDone = false;
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
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                Debug.Log("on ground"+isGrounded+"straightjump");
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                Debug.Log("inairstate"+(player.CurrentVelocity.y >= 13f));
                stateMachine.ChangeState(player.InAirState);
            }
        }
        if (isAbilityRunJumpDone)
        {
            if(isGrounded && player.rb.velocity.y < 0.01f)
            {
                Debug.Log("onground run jump");
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                Debug.Log("going run jump in air state"+player.CurrentVelocity.y+""+AnimBoolName);
                stateMachine.ChangeState(player.RunJumpInAirState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
