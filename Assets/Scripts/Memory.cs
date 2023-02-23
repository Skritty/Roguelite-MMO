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

    /*[HideInInspector]
    private DefaultMemorySO _default;
    [ShowInInspector, PropertyOrder(-104), FoldoutGroup("Core")]
    private DefaultMemorySO SetDefault
    {
        set
        {
            if (value == null)
            {
                _default.OnUpdate.RemoveListener(UpdateValuesToDefault);
                _default = null;
                return;
            }
            if(value.Memory == null)
            {
                Debug.LogWarning("Default Memory type is not set!");
                return;
            }
            if(!GetType().IsAssignableFrom(value.Memory.GetType()))
            {
                Debug.LogWarning($"Default Memory type {value.Memory.GetType()} does not match type {GetType()}!");
                return;
            }
            _default = value;
            _default.OnUpdate.AddListener(UpdateValuesToDefault);
            UpdateValuesToDefault();
        }
        get => _default;
    }*/
    /*private void UpdateValuesToDefault()
    {
        if (_default == null) return;
        foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            field.SetValue(this, field.GetValue(_default.Memory));
        }
    }*/

    /// <summary>
    /// Pseudo-Inheritance where the memory can have multiple different origins.
    /// </summary>
    [NonSerialized, OdinSerialize, ShowInInspector, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<Memory>()", HideChildProperties = true), PropertyOrder(-103), FoldoutGroup("Core")]
    protected List<Memory> parentMemories = new List<Memory>();

    /// <summary>
    /// Find if this memory might be a child of the memory in question.
    /// </summary>
    /// <param name="memory">Potential parent</param>
    /// <returns>Is the state a child of event e?</returns>
    public bool IsDerivativeOf(Memory memory)
    {
        if (Equals(memory)) return true;
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

    public object Clone()
    {
        return MemberwiseClone();
    }
}