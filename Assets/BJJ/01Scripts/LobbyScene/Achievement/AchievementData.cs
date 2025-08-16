using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType
{ 
    EnemyKill,
    TotalLevel,
    // classLevel
    ClearCount,
    HealAmount,
    CustomScript,
}

[Serializable]
public class AchievementStat
{
    public int enemyKill;
    public int clearCount;
    public int healAmount;

    public Dictionary<int, AchievementSaveData> achievementData;
}

[Serializable]
public class AchievementData
{
    public int achievementID;
    public AchievementType achievementType;
    public string achievementDescript;
    public int targetValue;
}

[Serializable]
public class AchievementSaveData
{
    public int achievementID;
    public bool isUnlocked;
}