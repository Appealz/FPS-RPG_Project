using System.Collections.Generic;
using UnityEngine;

public static class AnimHash
{
    private static Dictionary<string, int> animHashMap = new Dictionary<string, int>()
    {
        {"IsMove" , Animator.StringToHash("IsMove") },
        {"OnUse", Animator.StringToHash("OnUse") },
        {"OnReload", Animator.StringToHash("OnReload") },
        {"OnSkill", Animator.StringToHash("OnSkill")},
        {"OnAttack", Animator.StringToHash("OnAttack") },
    };

    public static int GetAnimHash(string type)
    {
        if(animHashMap.TryGetValue(type, out var hash))
            return hash;

        Debug.Log($"AnimHash.cs - GetAnimHash() - {type} is Non AnimParam");
        return -1;
    }
}
