using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIdleState : StateBase
{
    private float _curTime;
    private float _delayTime;
    private bool _running;
    public FishIdleState(StateMachine machine):base(machine)
    {
   
    }

    public override void Enter()
    {
        _curTime = 0;
        _delayTime = Random.Range(1f,2f);
        _running = false;
    }

    public override void Exit()
    {
        
    }

    public override void Update(float dt)
    {
        if(!_running)
        {
            _curTime += dt;
            if(_curTime >= _delayTime)
            {
                _running = true;
                _machine.ChangeState(EStateID.FishPatrol);
            }
        }
    }
}
