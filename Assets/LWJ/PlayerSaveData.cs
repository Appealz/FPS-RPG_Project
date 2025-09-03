using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    // 재화, 아이템리스트, 업적, 직업별 데이터(클래스(직업별 레벨, 스탯, 특전, 캐릭별로 마지막 장착아이템정보))
    public float currency;
    // 아이템리스트는 해금여부
    public Dictionary<int, bool> unlockedItems;
    public AchievementStat achievementData;
    public Dictionary<string, ClassData> classDatas = new Dictionary<string, ClassData>();
}

[Serializable]
public class AchivementProgress
{
    public int achivementID;
    public bool unlocked;
    public string unlockedTime;

    // 필요시 조건별 진행도 저장
}