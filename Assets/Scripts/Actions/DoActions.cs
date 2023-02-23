using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class DoActions : Reaction<InternalEvent>
{
    [SerializeReference, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<Event>()")]
    private List<Action> actions = new List<Action>();

    public override void Initialize()
    {
        base.Initialize();
        Debug.Log(Host);
        actions = actions.Select(a => a.Instance<Action>(Host)).ToList();
    }

    public override void Logic()
    {
        Debug.Log("Doing actions");
        foreach (Action action in actions)
        {
            if(action.IsValid())
                action.Logic();
        }
    }
}