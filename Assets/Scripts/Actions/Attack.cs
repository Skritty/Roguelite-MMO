using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action
{
    [SerializeReference]
    private Hit hit;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private bool clearTarget = true;

    private Target target;

    public override bool IsValid()
    {
        return Host.TryFetchMemory(out target) && Vector3.Distance(Host.transform.position, target.Host.transform.position) <= attackRange;
    }

    public override void Logic()
    {
        Debug.Log("Target "+target.Host);
        hit.Trigger();
        //if (clearTarget) Host.RemoveMemory(target);
    }
}
