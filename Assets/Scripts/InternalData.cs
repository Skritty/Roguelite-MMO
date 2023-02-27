using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public abstract class InternalData : Memory, IHostActor
{
    private Actor _host;
    [ShowInInspector, ReadOnly, PropertyOrder(-99), FoldoutGroup("Core")]
    public virtual Actor Host
    {
        get
        {
            return _host;
        }
        set
        {
            _host = value;
        }
    }

    public T Clone<T>(Actor host) where T : InternalEvent
    {
        T copy = Clone<T>(false);
        copy.Host = host;
        copy.Initialize();
        return copy;
    }
}
