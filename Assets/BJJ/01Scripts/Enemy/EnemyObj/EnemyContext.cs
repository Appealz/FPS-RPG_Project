using UnityEngine;

public interface IEnemyContextReadable
{
    string enemyName { get; }
    ITargetable curTarget { get; }
    float curHP { get; }
    float attackRange { get; }
    float attackSpeed { get; }

    float moveSpeed { get; }

    float damage { get; }
}

public interface IEnemyContextWriteable : IEnemyContextReadable
{
    void SetTarget(ITargetable target);

    void StatUpdate(StatManager stat);

    void SetEnemyName(string name);
}

public class EnemyContext : MonoBehaviour, IEnemyContextWriteable
{
    public string enemyName { get; private set; }
    public ITargetable curTarget { get; private set; }
    public float curHP { get; private set; }
    public float attackRange {  get; private set; }
    public float attackSpeed { get; private set; }
    public float moveSpeed { get; private set; }
    public float damage { get; private set; }

    public void SetEnemyName(string name)
    {
        enemyName = name;
    }

    public void SetTarget(ITargetable target)
    {
        curTarget = target;
    }

    public void StatUpdate(StatManager stat)
    {
        curHP = stat.CurHP;
        attackRange = stat.GetStat(StatType.AttackRange);
        attackSpeed = stat.GetStat(StatType.AttackSpeed);
        moveSpeed = stat.GetStat(StatType.MoveSpeed);
        damage = stat.GetStat(StatType.AttackDamage);
    }
}
