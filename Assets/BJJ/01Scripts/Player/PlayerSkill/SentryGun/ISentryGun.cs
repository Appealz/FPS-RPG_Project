using UnityEngine;

public interface ISentryGunWeapon
{
    void InitWeapon(Transform mount);
    void OnAttack();
    void OffAttack();

    void WeaponUpdate();
}

public interface ISentryGunReadableContext
{
    GameObject owner { get; }
    IEnemyTargetable target { get; }

    float GetStat(StatType type);
}

public interface ISentryGunWriteableContext : ISentryGunReadableContext
{
    void NewTarget(IEnemyTargetable newTarget);
    void Init(GameObject owner,StatManager statManager);
}
