using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : LocomotionState
{
    [SerializeField, FoldoutGroup("State")]
    private InternalEvent target;
    [SerializeField, FoldoutGroup("State")]
    private float movementSpeed;
    public override bool IsValid()
    {
        target = Host.FetchMemory(target).IfNull(target);
        return target != null && !target.IsOriginal;
    }

    protected override void OnFixedUpdate()
    {
        Debug.Log("Moving");
        Vector3 dir = (target.Host.transform.position - Host.transform.position).normalized;
        Host.transform.position += dir * movementSpeed * Time.fixedDeltaTime;
    }
}
