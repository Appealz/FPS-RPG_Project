using UnityEngine;

public interface IItemCtrl
{
    void Init();
    void Equip(IItem item);
    void UseCurrentItem();
    void ReloadWeapon();
    void Drop();
    void SetEnable(bool isOn);
    void SetReloadEnable(bool isOn);
}
