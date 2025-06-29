using System.Collections.Generic;
using UnityEngine;

public interface IEnemyWeapon
{
    void OnAttack(Transform attackOrigin,float range, float damage);
    IEnemyWeapon Clone();
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

public class EnemyMeleeAttackWeapon : IEnemyWeapon
{
    public IEnemyWeapon Clone()
    {
        return new EnemyMeleeAttackWeapon();
    }

    public void OnAttack(Transform attackOrigin, float range, float damage)
    {
        
    }
}

public class EnemyRangeAttackWeapon : IEnemyWeapon
{
    public IEnemyWeapon Clone()
    {
        return new EnemyRangeAttackWeapon();
    }

    public void OnAttack(Transform attackOrigin, float range, float damage)
    {
        if(Physics.Raycast(attackOrigin.position, attackOrigin.forward, out var hit, range, LayerMask.GetMask("Player")))
        {
            EventBus_Damage.Publish(new DamageInfo(attackOrigin.gameObject, hit.collider.gameObject, damage, null));
        }
    }
}

public class EnemySuicideWeapon : IEnemyWeapon
{
    public IEnemyWeapon Clone()
    {
        return new EnemySuicideWeapon();
    }

    public void OnAttack(Transform attackOrigin, float range, float damage)
    {
        Collider[] hits = Physics.OverlapSphere(attackOrigin.position, range, LayerMask.GetMask("Player"));
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
            EventBus_Damage.Publish(new DamageInfo(attackOrigin.gameObject, t, damage, null));
        }
    }
}