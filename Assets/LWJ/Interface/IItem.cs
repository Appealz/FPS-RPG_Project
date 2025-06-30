using UnityEngine;

public interface IItem
{
    // todo : SO ¡÷¿‘.
    
    void Use();
    bool useable { get; }
    AnimationClip useClip { get; }    
    AnimationClip reloadClip { get; }   
    AnimEventData useAnimData { get; }
    
}