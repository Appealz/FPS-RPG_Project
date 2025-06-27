using UnityEngine;

public interface IEnemyWeapon
{
    void OnAttack(Transform attackOrigin,float range, float damage);
}

public class EnemyMeleeAttackWeapon : IEnemyWeapon
{
    public void OnAttack(Transform attackOrigin, float range, float damage)
    {
        
    }
}

public class EnemyRangeAttackWeapon : IEnemyWeapon
{
    public void OnAttack(Transform attackOrigin, float range, float damage)
    {
        
    }
}

public class EnemySuicideWeapon : IEnemyWeapon
{
    public void OnAttack(Transform attackOrigin, float range, float damage)
    {
        
    }
}