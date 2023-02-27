using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Linq;
using System.Reflection;
using Sirenix.Serialization;

/// <summary>
/// The root of all things that an Actor can memorize.
/// </summary>
[Serializable]
public abstract class Memory : ISerializationCallbackReceiver, ICloneable
{
    public static IEnumerable<T> GetAssignableInstanceEnumerable<T>() where T : Memory => typeof(T).Assembly.GetTypes().Where(type => !type.IsAbstract && (type == typeof(T) || type.IsSubclassOf(typeof(T)))).Select(type => (T)Activator.CreateInstance(type));

    /// <summary>
    /// Pseudo-Inheritance where the memory can have multiple different origins.
    /// </summary>
    [SerializeReference, ShowInInspector, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<Memory>()", HideChildProperties = true), PropertyOrder(-103), FoldoutGroup("Core")]
    protected List<Memory> parentMemories = new List<Memory>();

    /// <summary>
    /// Find if this memory might be a child of the memory in question.
    /// </summary>
    /// <param name="memory">Potential parent</param>
    /// <returns>Is the state a child of event e?</returns>
    public bool IsDerivativeOf(Memory memory)
    {
        //if (Equals(memory)) return true;
        if (GetType() == memory.GetType()) return true;
        if (parentMemories.Contains(memory)) return true;
        else
        {
            foreach (Memory p in parentMemories)
            {
                if (p.IsDerivativeOf(memory)) return true;
            }
        }
        return false;
    }

    public virtual void Initialize() { }
    public virtual void Deinitialize() { }
    public void OnBeforeSerialize() => OnValidate();
    public void OnAfterDeserialize() { }
    protected virtual void OnValidate() { }

    /// <summary>
    /// Clones the memory and reinitializes the clone.
    /// </summary>
    public virtual object Clone()
    {
        Memory m = MemberwiseClone() as Memory;
        m.Initialize();
        return m;
    }

    /// <summary>
    /// Clones the memory and reinitializes the clone.
    /// </summary>
    public virtual T Clone<T>(bool initialize = true) where T : Memory
    {
        Memory m = MemberwiseClone() as Memory;
        if(initialize) m.Initialize();
        return m as T;
    }
}