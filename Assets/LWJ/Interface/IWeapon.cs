using UnityEngine;

public interface IWeapon : IItem
{
    void InitWeaponData(WeaponData newData);
}

public interface IMeleeWeapon : IWeapon
{
    
}

public interface IRangeWeapon : IWeapon
{
    
    void Reload();
}
