using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : BaseState
{
    private float _horizontalInput;

    public Running(MovementPattern stateMachine) : base("Running", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");
        //transition to idle state if input = 0
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
        {
            stateMachine.ChangeState(((MovementPattern)stateMachine).idleState);
        }
    }
}
