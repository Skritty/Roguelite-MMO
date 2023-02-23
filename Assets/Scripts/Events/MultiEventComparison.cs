using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiEventComparisonBase<T> : Event where T : Event
{
    [SerializeField]
    private List<T> events = new List<T>();

    public override bool Equals(object other)
    {
        if (other is not T) return false;
        return events.Contains(other as T);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

