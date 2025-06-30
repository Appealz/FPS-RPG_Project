using System.Collections.Generic;
using UnityEngine;

public static class EnemyAnimEventDataManager
{
    private static Dictionary<string, AnimEventData> animEventDataMap = new Dictionary<string, AnimEventData>();

    /// <summary>
    /// todo) 어드레서블 구조로 수정할 여지가 매우매우 높음
    /// </summary>
    public static void InitEnemyAnimData()
    {
        AnimEventData[] datas = Resources.LoadAll<AnimEventData>("EnemyAnimEventData");
        foreach(var data in datas)
        {
            animEventDataMap[data.name] = data;
            Debug.Log("test");
        }
    }

    public static bool GetAnimEventData(string animName, out AnimEventData data)
    {
        if (animEventDataMap.TryGetValue(animName, out data))
        {
            return true;
        }

        Debug.Log($"EnemyAnimEventDataManager.cs - GetAnimEventData() - {animName} is Non data");
        data = null;
        return false;
    }
}
