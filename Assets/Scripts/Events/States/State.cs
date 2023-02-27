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
    [SerializeReference, AcceptDefaultMemorySO, FoldoutGroup("State")]
    protected State exitState;
    [SerializeReference, AcceptDefaultMemorySO, FoldoutGroup("State")]
    protected List<State> simultaniousStateFunctionality;
    [ShowInInspector, ReadOnly, FoldoutGroup("State")]
    private int tick;

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
    protected virtual void OnExit() { }

    public sealed override void Logic()
    {
        if (IsValid())
        {
            FixedUpdate();
        }
        
        if(tick++ >= tickDuration && !looping)
        {
            stateMachine.SetState(exitState);
        }
    }

    protected virtual void FixedUpdate() { }

    public T Clone<T>(Actor host, StateMachine sm) where T : State
    {
        T copy = Clone<T>(false);
        copy.Host = host;
        copy.stateMachine = sm;
        copy.Initialize();
        return copy;
    }
}
