using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    // 재화, 아이템리스트, 업적, 직업별 데이터(클래스(직업별 레벨, 스탯, 특전, 캐릭별로 마지막 장착아이템정보))
    public float currency;
    // 아이템리스트는 해금여부
    // public Dictionary<int, bool> unlockedItems;
    public int achievementID;
    public Dictionary<string, ClassData> classDatas;
}

// Save Load 받아오고
// 