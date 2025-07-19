using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    protected StateMachine _machine;

    public StateBase(StateMachine machine)
    {
        _machine = machine;
    }

    public abstract void Update();

    public abstract void Enter();

    public abstract void Exit();
}
