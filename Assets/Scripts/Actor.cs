using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;
using Sirenix.OdinInspector.Editor;

public class Actor : MonoBehaviour
{
    private Queue<Event> eventQueue = new Queue<Event>();
    [SerializeReference, AcceptDefaultMemorySO, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<InternalData>()"), ListDrawerSettings(HideRemoveButton = true)]
    private List<InternalData> internalProperties = new List<InternalData>();
    [SerializeReference, AcceptDefaultMemorySO, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<Memory>()"), ListDrawerSettings(HideRemoveButton = true)]
    private List<Memory> ingrainedMemories = new List<Memory>();
    public System.Action OnFixedUpdate;

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    #region Initialization
    private void Awake()
    {
        // Create shallow copies of contained memories to break any reference links
        InitializeProperties();
        InitializeMemories();
    }

    private void InitializeProperties()
    {
        foreach (InternalData data in internalProperties)
        {
            InternalData copy = data.Clone<InternalData>(false);
            copy.Host = this;
            eventMemory.Add(new InternalMemory(copy, -1));
            copy.Initialize();
        }
    }

    private void InitializeMemories()
    {
        foreach (Memory memory in ingrainedMemories)
        {
            Memory copy = memory.Clone<Memory>(false);
            if (copy is IHostActor) (copy as IHostActor).Host = this;
            eventMemory.Add(new InternalMemory(copy, -1));
            copy.Initialize();
        }
    }
    #endregion

    #region Memory
    [SerializeReference, ReadOnly, TableList]
    private List<InternalMemory> eventMemory = new List<InternalMemory>();

    [System.Serializable]
    public class InternalMemory
    {
        [TableColumnWidth(20), HideInInspector]
        public int memoryIntensity;
        [ShowInInspector]
        public Memory memory;

        public InternalMemory(Memory memory, int memoryIntensity)
        {
            this.memory = memory;
            this.memoryIntensity = memoryIntensity;
        }

        // TODO: Better memory searching
    }

    public void AddMemory(Memory e, int intensity = 10)
    {
        eventMemory.RemoveAll(memory =>
        {
            if (memory.memoryIntensity < 0) return false;
            return --memory.memoryIntensity == 0;
        });
        eventMemory.Add(new InternalMemory(e, intensity));
        if (eventMemory.Count > 100) eventMemory.RemoveAt(0);
    }

    public void RemoveMemory(Memory e)
    {
        eventMemory.RemoveAll(m => m.memory == e);
    }

    public T FetchMemory<T>() where T : Memory
    {
        Debug.Log($"{name} has event memory of count {eventMemory.Count}");
        foreach (InternalMemory memory in eventMemory)
        {
            if (typeof(T) == memory.memory.GetType())
            {
                //if (memory.memoryIntensity > 0 && --memory.memoryIntensity == 0) eventMemory.Remove(memory);
                return memory.memory as T;
            }
        }
        return null;
    }

    public T FetchMemory<T>(T e) where T : Memory
    {
        if (e == null) return null;
        foreach (InternalMemory memory in eventMemory)
        {
            if (e.Equals(memory.memory))
            {
                //if (memory.memoryIntensity > 0 && --memory.memoryIntensity == 0) eventMemory.Remove(memory);
                return memory.memory as T;
            }
        }
        return null;
    }

    public bool TryFetchMemory<T>(out T e) where T : Memory
    {
        foreach (InternalMemory memory in eventMemory)
        {
            if (typeof(T) == memory.memory.GetType())
            {
                //if (memory.memoryIntensity > 0 && --memory.memoryIntensity == 0) eventMemory.Remove(memory);
                e = memory.memory as T;
                return true;
            }
        }
        e = null;
        return false;
    }

    public void ClearMemory()
    {

    }
    #endregion
}
