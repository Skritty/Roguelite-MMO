using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using System.Linq;

/// <summary>
/// An event that happens to an Actor, or simply data about an Actor.
/// </summary>
[Serializable]
public class InternalEvent : Event, IHostActor
{
    private Actor _host;
    [ShowInInspector, ReadOnly, PropertyOrder(-99), FoldoutGroup("Core")]
    public virtual Actor Host
    {
        get
        {
            return _host;
        }
        set
        {
            _host = value;
        }
    }

    public T Clone<T>(Actor host) where T : InternalEvent
    {
        T copy = Clone<T>(false);
        copy.Host = host;
        copy.Initialize();
        return copy;
    }
}