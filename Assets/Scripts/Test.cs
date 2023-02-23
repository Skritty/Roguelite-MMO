using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Test : MonoBehaviour
{
    [AcceptDefaultMemorySO]
    [ShowDrawerChain]
    [SerializeReference]
    [ShowInInspector]
    public Memory memory;
}
