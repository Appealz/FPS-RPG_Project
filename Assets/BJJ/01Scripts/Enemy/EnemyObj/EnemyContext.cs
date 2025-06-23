using UnityEngine;

public interface IEnemyContextReadable
{
    ITargetable curTarget { get; }
    float curHP { get; }
    float attackRange { get; }
    float attackSpeed { get; }

}

public interface IEnemyContextWriteable : IEnemyContextReadable
{
    void SetTarget(ITargetable target);
}

public class EnemyContext : MonoBehaviour, IEnemyContextWriteable
{
    public ITargetable curTarget { get; private set; }
    public float curHP { get; private set; }
    public float attackRange {  get; private set; }
    public float attackSpeed { get; private set; }

    public void SetTarget(ITargetable target)
    {
        curTarget = target;
    }

    public void StatUpdate(StatManager stat)
    {
        curHP = stat.CurHP;
        attackRange = stat.GetStat(StatType.AttackRange);
        attackSpeed = stat.GetStat(StatType.AttackSpeed);
    }
}
