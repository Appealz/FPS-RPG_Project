using UnityEngine;

public interface IItemCtrl
{
    void Init();
    void Equip(IItem newItem);
    void UseCurrentItem();
    void ReloadWeapon();
    void Drop();
    void SetEnable(bool isOn);
}
