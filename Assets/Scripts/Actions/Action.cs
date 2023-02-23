using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Action : InternalEvent
{
    public abstract void Logic();
    public virtual bool IsValid()
    {
        return true;
    }
}

