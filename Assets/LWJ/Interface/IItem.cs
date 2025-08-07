using UnityEngine;

public interface IItem
{
    // todo : SO ¡÷¿‘.
    int itemID { get; }
    void Use();
    bool useable { get; }
    AnimEventData useAnimData { get; }

    void InitData(ItemData newData);

    CurrentData GetItemCurrentData();

}