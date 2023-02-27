using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : LocomotionState
{
    [SerializeField, FoldoutGroup("State")]
    private Target target;
    [SerializeField, FoldoutGroup("State")]
    private float movementSpeed;
    public override bool IsValid()
    {
        return Host.TryFetchMemory(out target);
    }

    protected override void FixedUpdate()
    {
        Vector3 dir = (target.Host.transform.position - Host.transform.position).normalized;
        Host.transform.position += dir * movementSpeed * Time.fixedDeltaTime;
    }
}
