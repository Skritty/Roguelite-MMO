using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The most basic form of event. Events become instances and propagate their creation out to listeners.
/// </summary>
[Serializable]
public class Event : Memory
{
    #region Static
    private static Dictionary<Type, List<System.Action<Event>>> _listeners = new Dictionary<Type, List<System.Action<Event>>>();
    public static void SubscribeToEvent(Event e, System.Action<Event> action)
    {
        Type t = e.GetType();
        if (!_listeners.ContainsKey(t)) _listeners.Add(t, new List<System.Action<Event>>());
        _listeners[t].Add(action);
    }

    public static void UnsubscribeFromEvent(Event e, System.Action<Event> action)
    {
        Type t = e.GetType();
        if (!_listeners.ContainsKey(t)) return;
        _listeners[t].Remove(action);
        if (_listeners[t].Count == 0) _listeners.Remove(t);
    }
    #endregion

    /// <summary>
    /// Instance and propagate the event immediately.
    /// </summary>
    public void Trigger()
    {
        Clone<Event>().Propagate();
    }

    public void Propagate()
    {
        Debug.Log($"{GetType()} has listeners? {_listeners.ContainsKey(GetType())}");
        HashSet<System.Action<Event>> completed = new HashSet<System.Action<Event>>();
        if (_listeners.ContainsKey(GetType()))
            foreach (System.Action<Event> a in _listeners[GetType()])
            {
                completed.Add(a);
                a.Invoke(this);
            }

        foreach(Event e in parentMemories)
        {
            e.Propagate(this, completed);
        }
    }

    private void Propagate(Event self, HashSet<System.Action<Event>> completed)
    {
        if (_listeners.ContainsKey(GetType()))
            foreach (System.Action<Event> a in _listeners[GetType()])
            {
                if (completed.Contains(a)) continue;
                completed.Add(a);
                a.Invoke(self);
            }

        foreach (Event e in parentMemories)
        {
            e.Propagate(self, completed);
        }
    }
}