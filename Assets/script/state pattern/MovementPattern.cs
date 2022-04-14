using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : StateMachine
{
    public Idle idleState;
    public Running runningState;

    public Rigidbody2D rig;
    private void Awake()
    {
        idleState = new Idle(this);
        runningState = new Running(this);
    }
    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
