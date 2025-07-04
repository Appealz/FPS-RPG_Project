using UnityEngine;

public interface IItemCtrl
{
    void Init();
    void Equip(int itemID);
    void UseCurrentItem();
    void ReloadWeapon();
    void Drop();
    void SetEnable(bool isOn);
    void SetReloadEnable(bool isOn);
}
