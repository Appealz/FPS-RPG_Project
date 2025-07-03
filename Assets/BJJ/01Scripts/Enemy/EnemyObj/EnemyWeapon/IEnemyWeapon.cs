using System.Collections.Generic;
using UnityEngine;

public interface IEnemyWeapon
{
    void OnAttack(float range, float damage);
    IEnemyWeapon Clone();
    void Init(GameObject owner);
}

public interface IAttackPointInjectable
{
    void SetAttackPoint(Transform point);
}

public static class EnemyWeaponFactory
{
    /// <summary>
    /// 몬스터 이름 / 공격 로직 객체 Map
    /// </summary>
    private static Dictionary<string, IEnemyWeapon> weaponMap = new Dictionary<string, IEnemyWeapon>()
    {
        {"MeleeRobot", new EnemyMeleeAttackWeapon() },
        {"RangeRobot", new EnemyRangeAttackWeapon() },
        {"SuicideWeapon", new EnemySuicideWeapon() }
    };

    public static IEnemyWeapon GetEnemyWeapon(string name)
    {
        if(weaponMap.TryGetValue(name, out var weapon))
            return weapon.Clone();

        Debug.Log($"EnemyWeaponFactory.cs - GetEnemyWeapon() - {name} is Error Name");
        return null;
    }
}

public class EnemyMeleeAttackWeapon : IEnemyWeapon, IAttackPointInjectable
{
    private GameObject owner;
    private Transform attackPoint;

    public IEnemyWeapon Clone()
    {
        return new EnemyMeleeAttackWeapon();
    }

    public void Init(GameObject owner)
    {
        this.owner = owner;
    }

    public void OnAttack(float range, float damage)
    {
        Vector3 center = attackPoint.position + attackPoint.forward * (range * 0.5f);
        var hits = Physics.OverlapSphere(center, range, LayerMask.GetMask("Player"));

        foreach(var hit in hits)
        {
            EventBus_Damage.Publish(new DamageInfo(owner, hit.gameObject, damage, null));
        }
    }

    public void SetAttackPoint(Transform point)
    {
        attackPoint = point;
    }
}

public class EnemyRangeAttackWeapon : IEnemyWeapon, IAttackPointInjectable
{
    private GameObject owner;
    private Transform attackPoint;

    public IEnemyWeapon Clone()
    {
        return new EnemyRangeAttackWeapon();
    }

    public void Init(GameObject owner)
    {
        this.owner = owner;
    }

    public void OnAttack(float range, float damage)
    {
        if(Physics.Raycast(attackPoint.position, attackPoint.forward, out var hit, range, LayerMask.GetMask("Player")))
        {
            Debug.Log($"{hit.collider.name}");
            EventBus_Damage.Publish(new DamageInfo(owner, hit.collider.gameObject, damage, null));
        }
    }

    public void SetAttackPoint(Transform point)
    {
        attackPoint = point;
    }
}

public class EnemySuicideWeapon : IEnemyWeapon
{
    private GameObject owner;

    public IEnemyWeapon Clone()
    {
        return new EnemySuicideWeapon();
    }

    public void Init(GameObject owner)
    {
        this.owner = owner;
    }

    public void OnAttack(float range, float damage)
    {
        Collider[] hits = Physics.OverlapSphere(owner.transform.position, range, LayerMask.GetMask("Player"));
        HashSet<GameObject> targets = new HashSet<GameObject>();

        foreach(var hit in hits)
        {
            if(hit.TryGetComponent<IHitPart>(out IHitPart part))
            {
                targets.Add(part.owner.ReciverGO);
            }
        }

        foreach(var t in targets)
        {
            EventBus_Damage.Publish(new DamageInfo(owner, t, damage, null));
        }
    }
}