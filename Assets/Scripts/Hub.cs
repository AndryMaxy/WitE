using System;
using System.Collections.Generic;

public static class Hub
{
    private static Dictionary<string, Dictionary<Guid, Action<object>>> listeners = new Dictionary<string, Dictionary<Guid, Action<object>>>();

    public static void Subscribe(List<Subscription> subs, string eventName, Action<object> handler)
    {
        Subscription sub = Subscribe(eventName, handler);
        subs.Add(sub);
    }

    public static Subscription Subscribe(string eventName, Action<object> handler)
    {
        if (!listeners.ContainsKey(eventName))
        {
            listeners.Add(eventName, new Dictionary<Guid, Action<object>>());
        }

        Guid guid = Guid.NewGuid();
        listeners[eventName].Add(guid, handler);

        return new Subscription(eventName, guid);
    }

    public static void Unsubsribe(Subscription subscription)
    {
        if (!listeners.ContainsKey(subscription.EventName)) return;

        listeners[subscription.EventName].Remove(subscription.Guid);

        if (listeners[subscription.EventName].Count == 0)
        {
            listeners.Remove(subscription.EventName);
        }
    }

    public static void Unsubsribe(List<Subscription> subscriptions)
    {
        subscriptions.ForEach(sub => Unsubsribe(sub));
    }

    public static void Publish(string publishedEvent)
    {
        Publish(publishedEvent, new object());
    }

    public static void Publish(string publishedEvent, object data)
    {
        if (!listeners.ContainsKey(publishedEvent)) return;

        foreach (var action in listeners[publishedEvent].Values)
        {
            action.Invoke(data);
        }
    }

    public static void Clear()
    {
        listeners.Clear();
    }

    public class Subscription
    {
        public string EventName { get; }
        public Guid Guid { get; }

        public Subscription(string eventName, Guid guid)
        {
            EventName = eventName;
            Guid = guid;
        }
    }
}