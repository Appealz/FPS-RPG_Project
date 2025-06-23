using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    Transform GetTransform();
    bool IsAlive { get; }
}

public class PlayerScanManager : DestroySingleton<PlayerScanManager>
{
    private List<ITargetable> targets = new List<ITargetable>();

    public void RegisterTarget(ITargetable newTarget) => targets.Add(newTarget);
    public void UnRegisterTarget(ITargetable t) => targets.Remove(t);

    public ITargetable FindNearst(Vector3 pos)
    {
        float dir = Mathf.Infinity;
        ITargetable curT = null;

        foreach (var target in targets)
        {
            float temp = (target.GetTransform().position - pos).sqrMagnitude;
            if(dir > temp)
            {
                dir = temp;
                curT = target;
            }
        }

        return curT;
    }
}
