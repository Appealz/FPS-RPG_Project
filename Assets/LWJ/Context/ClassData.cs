using System.Collections.Generic;
using UnityEngine;

public class ClassData
{
    public int level;
    public float currentExp;
    public float hpStats;
    public float attackStats;
    public float moveSpeedStats;
    public List<int> unlockedPerks;      // 해금된 특전 ID
    public Dictionary<itemSlotType, int> equippedItemDictionary;    // 마지막 장착 아이템 ID 리스트 (ex: 무기, 방어구, 스킬 등)

    public ClassData(Dictionary<itemSlotType, int> newEquipData, int level)
    {
        this.level = level;
        equippedItemDictionary = newEquipData;
    }
}
