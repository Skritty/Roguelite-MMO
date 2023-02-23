using UnityEngine;

[System.Serializable]
public abstract class InternalData : Memory, IHostActor
{
    [System.NonSerialized, Sirenix.Serialization.OdinSerialize, HideInInspector]
    private Actor _host;
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

    /// <summary>
    /// Initializes the instance after setting the host.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="host"></param>
    /// <returns></returns>
    public virtual T Instance<T>(Actor host) where T : InternalData
    {
        T copy = default(T);
        copy.Host = host;
        copy.Initialize();
        return copy;
    }
}
