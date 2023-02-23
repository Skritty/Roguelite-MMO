using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action
{
    [SerializeField]
    private Hit hit;
    [SerializeField]
    private float attackRange;
    public bool clearTarget = true;

    private InternalEvent target;

    public override bool IsValid()
    {
        target = Host.FetchMemory(target).IfNull(target);
        Debug.Log(Vector3.Distance(Host.transform.position, target.Host.transform.position));
        return target != null && !target.IsOriginal && Vector3.Distance(Host.transform.position, target.Host.transform.position) <= attackRange;
    }

    public override void Logic()
    {
        Debug.Log("Target "+target.Host);
        hit.Trigger(target.Host);
        //if (clearTarget) Host.RemoveMemory(target);
    }
}
