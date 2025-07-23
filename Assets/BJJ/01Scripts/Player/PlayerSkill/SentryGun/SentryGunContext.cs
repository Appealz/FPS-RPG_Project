using System.Collections;
using UnityEngine;

public class SentryGunContext : MonoBehaviour, ISentryGunWriteableContext
{
    private StatManager statManager;

    public IEnemyTargetable target { get; private set; }

    public GameObject owner { get; private set; }

    public float GetStat(StatType type)
    {
        return statManager.GetStat(type);
    }

    public void NewTarget(IEnemyTargetable newTarget)
    {
        target = newTarget;
    }

    public void Init(GameObject newOwner,StatManager statManager)
    {
        owner = newOwner;
        this.statManager = statManager;
    }
}
