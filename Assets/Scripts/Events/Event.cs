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
    private static Dictionary<Event, List<System.Action<Event>>> _listeners = new Dictionary<Event, List<System.Action<Event>>>();
    public static void SubscribeToEvent(Event e, System.Action<Event> action)
    {
        if (!_listeners.ContainsKey(e.Original)) _listeners.Add(e.Original, new List<System.Action<Event>>());
        _listeners[e.Original].Add(action);
    }

    public static void UnsubscribeFromEvent(Event e, System.Action<Event> action)
    {
        if (!_listeners.ContainsKey(e.Original)) return;
        _listeners[e.Original].Remove(action);
        if (_listeners[e.Original].Count == 0) _listeners.Remove(e.Original);
    }
    #endregion

    [SerializeField, HideInInspector]
    private Event _original;
    public Event Original
    {
        get
        {
            if (_original == null) return this;
            else return _original;
        }
    }
    public bool IsOriginal => _original == null;

    /// <summary>
    /// Instance and propagate the event immediately.
    /// </summary>
    public void Trigger()
    {
        Instance<Event>().Propagate();
    }

    /// <summary>
    /// Creates an instance copy of this event scriptable object. If it is already an instance, returns this.
    /// </summary>
    /// <returns>Instance of the event</returns>
    public T Instance<T>() where T : Event
    {
        if (Original != null) return this as T;
        T copy = InstanceNoInit<T>();
        copy.Initialize();
        return copy;
    }

    protected T InstanceNoInit<T>() where T : Event
    {
        T copy = Clone() as T;
        copy._original = this;
        return copy;
    }

    public void Propagate()
    {
        //Debug.Log($"{name} has listeners? {_listeners.ContainsKey(Original)}");
        HashSet<System.Action<Event>> completed = new HashSet<System.Action<Event>>();
        if (_listeners.ContainsKey(Original))
            foreach (System.Action<Event> a in _listeners[Original])
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
        if (_listeners.ContainsKey(Original))
            foreach (System.Action<Event> a in _listeners[Original])
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

    /*public override bool Equals(object other)
    {
        if (other == null || other is not Event) return false;
        return Original == (other as Event).Original;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }*/
}