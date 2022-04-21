using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{

    private int normalInputX;

    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        normalInputX = player.InputHandler.normalInputX;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("landingstate");

        if (!isExitingState)
        {
            if (normalInputX != 0)
            {
                Debug.Log("xinput != 0 from playerlandstate");
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                Debug.Log("isanimationfinished" + isAnimationFinished + "landstate");
                stateMachine.ChangeState(player.IdleState);
            }
        }
        
    }
}
