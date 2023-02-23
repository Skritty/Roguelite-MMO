using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class State : Action
{
    [System.NonSerialized]
    protected StateMachine stateMachine;
    [SerializeField, FoldoutGroup("State")]
    protected string animationName;
    [SerializeField, FoldoutGroup("State")]
    protected float animationTransitionTime;
    [SerializeField, FoldoutGroup("State")]
    protected int tickDuration;
    [SerializeField, FoldoutGroup("State")]
    protected bool looping;
    [SerializeField, FoldoutGroup("State")]
    protected State exitState;
    [System.NonSerialized, Sirenix.Serialization.OdinSerialize, FoldoutGroup("State")]
    protected List<State> simultaniousStateFunctionality;
    [ShowInInspector, ReadOnly, FoldoutGroup("State")]
    private int tick;
    public T Instance<T>(Actor host, StateMachine sm) where T : State
    {
        if (!IsOriginal) return this as T;
        State copy = Instance<State>(host);
        copy.stateMachine = sm;
        copy.Initialize();
        return copy as T;
    }

    public sealed override void Initialize() { }
    public sealed override void Deinitialize() { }
    public void Enter()
    {
        tick = 0;
        OnEnter();
    }
    protected virtual void OnEnter() { }
    public void Exit()
    {
        OnExit();
    }
    protected virtual void OnExit() 
    {
        
    }

    public sealed override void Logic()
    {
        if (!IsValid()) return;
        OnFixedUpdate();
        if(tick++ >= tickDuration && !looping)
        {
            stateMachine.SetState(exitState);
        }
    }

    protected virtual void OnFixedUpdate() { }
}
