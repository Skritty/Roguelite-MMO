using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAdjustment : Reaction<Hit>
{
    [SerializeField]
    private Color hurtColor;

    private StatData stats;
    private VisualData visuals;

    public override bool IsValid()
    {
        if (trigger.Host != Host) return false;
        stats = Host.FetchMemory<StatData>().IfNull(stats);
        visuals = Host.FetchMemory<VisualData>();
        return stats != null;
    }
    public override void Logic()
    {
        stats.hp -= trigger.damage;
        Host.StartCoroutine(Hurt());
    }

    private IEnumerator Hurt()
    {
        Color c = visuals.renderer.material.color;
        visuals.renderer.material.color = hurtColor;
        yield return new WaitForSeconds(.5f);
        visuals.renderer.material.color = c;
    }
}