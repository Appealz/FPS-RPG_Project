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
    public List<int> equippedItems;

    /// <summary>
    /// 플레이어의 장착 무기 리스트(딕셔너리), 플레이어 레벨
    /// </summary>
    /// <param name="newEquipData"></param>
    /// <param name="level"></param>
    public ClassData(Dictionary<itemSlotType, int> newEquipData, int level, List<int> newItems)
    {
        this.level = level;
        equippedItemDictionary = newEquipData;
        equippedItems = newItems;
    }

    public int GetEquippedItemID(itemSlotType slotType)
    {
        equippedItemDictionary.TryGetValue(slotType, out int id);
        return id;
    }

    public List<int> GetEquippedItems()
    {
        return equippedItems;
    }
}
