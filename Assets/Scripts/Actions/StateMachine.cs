using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class StateMachine : InternalData
{
    [SerializeReference, AcceptDefaultMemorySO, Tooltip("The first state in this list will be the initial state")]
    private List<State> possibleStates = new List<State>();
    [SerializeReference, ShowInInspector, ReadOnly]
    private State currentState;

    public override void Initialize()
    {
        possibleStates = possibleStates.Select(s => s.Clone<State>(Host, this)).ToList();
        Host.StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForFixedUpdate();
            if(possibleStates.Count > 0)
                SetState(possibleStates[0]);
        }
        Host.OnFixedUpdate += FixedUpdate;
    }

    public T Instance<T>()
    {
        return default(T);
    }

    public void FixedUpdate()
    {
        currentState?.Logic();
    }

    /// <summary>
    /// Sets a derivative matching state to be the current state if it exists from the state machine. 
    /// Uses the first state found in order if there are multiple with the same derivative nature.
    /// </summary>
    /// <param name="state">The state to set</param>
    /// <param name="addNew">Forcibly set the state to a new instance and add it to this state</param>
    /// <returns>Was the state change successful?</returns>
    public bool SetState(State state, bool addNew = false)
    {
        if (state == null) return false;
        if (addNew && !possibleStates.Contains(state))
        {
            possibleStates.Add(state);
            ChangeState(state);
            return true;
        }
        foreach (State s in possibleStates)
        {
            if (s.IsDerivativeOf(state))
            {
                ChangeState(s);
                return true;
            }
        }
        return false;
    }

    private void ChangeState(State state)
    {
        currentState?.Exit();
        currentState = state;
        Debug.Log(currentState);
        currentState?.Enter();
        currentState?.Propagate();
    }
}
