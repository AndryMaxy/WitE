using UnityEngine;
using System.Collections.Generic;
using System;

public class EventBehaviour : MonoBehaviour
{

    protected List<Hub.Subscription> subs = new List<Hub.Subscription>();

    protected void AddEvent<T>(string eventName, Action<T> action)
    {
        Hub.Subscribe(subs, eventName, action);
    }

    protected void AddEvent(string eventName, Action action)
    {
        AddEvent<object>(eventName, obj => action());
    }

    protected void OnDestroy()
    {
        Hub.Unsubsribe(subs);
    }
}