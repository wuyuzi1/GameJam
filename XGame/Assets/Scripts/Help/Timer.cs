using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    private float _curTime;
    private float _duration;
    private Action _action;
    private bool _isRunning;
    public bool IsRunning
    { get 
        { 
            return _isRunning; 
        } 
    }

    public void SetTimer(float duration,Action action)
    {
        _duration = duration; 
        _action = action;
        _isRunning = true;
    }

    public void Update(float dt)
    {
        if(!_isRunning)
        {
            return;
        }
        _curTime += dt;
        if(_curTime>=_duration)
        {
            if(_action!=null)
            {
                _action.Invoke();
            }
            _isRunning = false;
        }    
    }
}
