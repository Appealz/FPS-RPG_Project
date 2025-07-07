using UnityEngine;

public interface IItem : IPoolLabel
{
    // todo : SO ¡÷¿‘.
    int itemID { get; }
    void Use();
    bool useable { get; }
    AnimationClip useClip { get; }
    AnimationClip reloadClip { get; }
    AnimEventData useAnimData { get; }

    void InitData(ItemData newData);

    CurrentData GetItemCurrentData();

}