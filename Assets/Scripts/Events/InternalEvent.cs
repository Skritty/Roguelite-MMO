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
    [NonSerialized, Sirenix.Serialization.OdinSerialize]
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

    /// <summary>
    /// Instance and propagate the event immediately while setting a host.
    /// </summary>
    public void Trigger(Actor host)
    {
        Instance<InternalEvent>(host).Propagate();
    }

    /// <summary>
    /// Initializes the instance after setting the host.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="host"></param>
    /// <returns></returns>
    public virtual T Instance<T>(Actor host) where T : InternalEvent
    {
        if (!IsOriginal) return this as T;
        T copy = InstanceNoInit<T>();
        copy.Host = host;
        copy.Initialize();
        return copy;
    }
}