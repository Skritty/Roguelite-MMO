using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FindTarget : Action
{
    [SerializeField, Sirenix.OdinInspector.ReadOnly]
    private InternalEvent target;
    [SerializeField]
    private bool targetSelf;
    public override void Logic()
    {
        Actor t = null;
        if (targetSelf)
        {
            t = Host;
        }
        else
        {
            foreach (Actor actor in Object.FindObjectsOfType<Actor>())
            {
                if (actor != Host)
                {
                    // The most basic target finding
                    t = actor;
                    break;
                }
            }
        }
        
        Target target = new Target();
        target.Host = Host;
        target.target = t;
        Host.AddMemory(target);
    }
}
