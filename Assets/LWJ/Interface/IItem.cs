using UnityEngine;

public interface IItem
{
    // todo : SO ¡÷¿‘.
    int itemID { get; }
    void Use();
    bool useable { get; }
    AnimationClip useClip { get; }
    AnimationClip reloadClip { get; }
    AnimEventData useAnimData { get; }
    
}