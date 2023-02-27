using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An event with functionality that listens to an event of type T.
/// </summary>
/// <typeparam name="T">Event Type</typeparam>
[System.Serializable]
public abstract class Reaction<T> : Action where T : Event
{
    [SerializeReference, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<Event>()", HideChildProperties = true), Tooltip("If left null, will not subscribe to a trigger."), FoldoutGroup("Core")]
    protected T trigger;

    public override void Initialize()
    {
        Debug.Log(trigger);
        Subscribe(trigger);
    }

    public override void Deinitialize()
    {
        Unsubscribe(trigger);
    }

    /// <summary>
    /// Subscribe to the trigger event to handle logic when it is propagated.
    /// </summary>
    /// /// <param name="e">Event to subscribe to</param>
    public void Subscribe(T e)
    {
        if (e == null) return;
        SubscribeToEvent(e, Handle);
    }

    /// <summary>
    /// Unsubscribe from the trigger event.
    /// </summary>
    /// /// <param name="e">Event to unsubscribe from</param>
    public void Unsubscribe(T e)
    {
        if (e == null) return;
        UnsubscribeFromEvent(e, Handle);
    }

    /// <summary>
    /// Handles logic if action is valid. Can be called manually or by subscribing to an event propagation.
    /// Creates an instance, checks if the action is valid, calls the logic, then propagates the action.
    /// </summary>
    /// <param name="cause">The cause of this Action</param>
    private void Handle(Event cause)
    {
        trigger = cause as T;
        if (!IsValid()) return;
        Logic();
        Propagate();
    }
}