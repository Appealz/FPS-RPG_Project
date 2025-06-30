using UnityEngine;

public interface IWeapon : IItem
{    
    void InitWeaponData(WeaponData newData);
    void Attack();
}

public interface IMeleeWeapon : IWeapon
{
    
}

public interface IRangeWeapon : IWeapon
{
    AnimEventData reloadAnimData { get; }
    void StartReload();
    void Reload();
    void CancelReload();
}
