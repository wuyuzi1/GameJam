using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : Singleton<EventCenter>
{
    private Dictionary<string, Action<object[]>> _events;

    public override void Init()
    {
        _events = new Dictionary<string, Action<object[]>>();
    }

    public void AddListener(string eventname,Action<object[]> action)
    {
        if(!_events.ContainsKey(eventname))
        {
            _events.Add(eventname, action);
        }
        else
        {
            _events[eventname] += action;
        }
    }

    public void RemoveListener(string eventname, Action<object[]> action)
    {
        if(!_events.ContainsKey(eventname))
        {
            return;
        }
        else
        {
            _events[eventname] -= action;
        }
    }

    public void TriggerEvent(string eventname,params object[] args)
    {
        if(!_events.ContainsKey(eventname))
        {
            return;
        }
        else
        {
            _events[eventname].Invoke(args);
        }
    }
}
