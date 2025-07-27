using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public GameObject Holder;
    private StateBase _curState;
    public Dictionary<EStateID, StateBase> _stateDic;

    public StateMachine(GameObject holder)
    {
        Holder = holder;
        _stateDic = new Dictionary<EStateID,StateBase>();
    }

    public void AddState(EStateID stateID)
    {
        string className = stateID.ToString() + "State";
        Type type = Type.GetType(className);
        var state = Activator.CreateInstance(type,this) as StateBase;
        _stateDic.Add(stateID,state);
    }

    public void ChangeState(EStateID stateID)
    {
        if (_curState != null)
        {
            _curState.Exit();
        }
        _curState = _stateDic[stateID];
        _curState.Enter();
    }

    public void UpdateState(float dt)
    {
        if(_curState!=null)
        {
            _curState.Update(dt);
        }
    }
}
