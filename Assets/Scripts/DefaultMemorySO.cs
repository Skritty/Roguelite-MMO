using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Default Memory")]
public class DefaultMemorySO : ScriptableObject
{
    [SerializeReference, HideLabel, HideReferenceObjectPicker]
    public Memory Memory;
}
