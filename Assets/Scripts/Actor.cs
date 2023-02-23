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
    [SerializeReference, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<InternalData>()"), ListDrawerSettings(HideRemoveButton = true)]
    private List<InternalData> internalProperties = new List<InternalData>();
    [SerializeField, HideInInspector]
    private List<InternalData> existingProperties = new List<InternalData>();
    [SerializeReference, HideReferenceObjectPicker, ValueDropdown("@Memory.GetAssignableInstanceEnumerable<Memory>()"), ListDrawerSettings(HideRemoveButton = true)]
    private List<Memory> ingrainedMemories = new List<Memory>();
    public System.Action OnFixedUpdate;

    private void Start()
    {
        InitializeProperties();
        InitializeMemories();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    private void OnValidate()
    {
        for(int i = 0; i < internalProperties.Count; i++)
        {
            if (!existingProperties.Contains(internalProperties[i]))
            {
                //internalProperties[i] = Instantiate(internalProperties[i]); FIX
                internalProperties[i].Host = this;
                existingProperties.Add(internalProperties[i]);
            }
        }
    }

    private void InitializeProperties()
    {
        /*for (int i = 0; i < internalProperties.Count; i++)
        {
            eventMemory.Add(new InternalMemory(internalProperties[i], -1));
            internalProperties[i].Initialize();
        }*/
    }

    private void InitializeMemories()
    {
        foreach (Memory e in ingrainedMemories)
        {
            Memory copy = e.Clone() as Memory;//Instantiate(e); FIX
            if (copy is IHostActor) (copy as IHostActor).Host = this;
            eventMemory.Add(new InternalMemory(copy, -1));
            copy.Initialize();
        }
    }

    #region Memory
    [SerializeField, ReadOnly, TableList]
    private List<InternalMemory> eventMemory = new List<InternalMemory>();

    [System.Serializable]
    public class InternalMemory
    {
        [TableColumnWidth(20)]
        public int memoryIntensity;
        [InlineEditor]
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
                if (memory.memoryIntensity > 0 && --memory.memoryIntensity == 0) eventMemory.Remove(memory);
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
                if (memory.memoryIntensity > 0 && --memory.memoryIntensity == 0) eventMemory.Remove(memory);
                return memory.memory as T;
            }
        }
        return null;
    }

    public void ClearMemory()
    {

    }
    #endregion
}
