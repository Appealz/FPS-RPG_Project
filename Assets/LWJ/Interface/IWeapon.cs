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
    float weaponRecoil { get; }
    AnimEventData reloadAnimData { get; }
    void StartReload();
    void Reload();
    void CancelReload();
}

public interface IDroppable
{
    void Drop(Vector3 dropDir, float dropForce);
}