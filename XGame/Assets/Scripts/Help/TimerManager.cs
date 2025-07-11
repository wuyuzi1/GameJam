using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    public Queue<Timer> _freeTimers;
    public List<Timer> _workTimers;

    public override void Init()
    {
        _freeTimers = new Queue<Timer>();
        _workTimers = new List<Timer>();
        for(int i=0;i<10;i++)
        {
            _freeTimers.Enqueue(new Timer());
        }
    }

    public void GetOneTimer(float duration,Action action)
    {
        Timer timer = null;
        if(_freeTimers.Count<=0)
        {
            timer = new Timer();
        }
        else
        {
            timer = _freeTimers.Dequeue();
        }
        timer.SetTimer(duration, action);
        _workTimers.Add(timer);
    }

    public void UpdateTimer(float dt)
    {
        if(_workTimers.Count<=0)
        {
            return;
        }
        for(int i=_workTimers.Count-1;i>=0;i--)
        {
            _workTimers[i].Update(dt);
            if (_workTimers[i].IsRunning == false)
            {
                _freeTimers.Enqueue(_workTimers[i]);
                _workTimers.RemoveAt(i);
            }
        }
    }
}
