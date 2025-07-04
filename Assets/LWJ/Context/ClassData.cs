using System.Collections.Generic;
using UnityEngine;

public class ClassData
{
    public int level;
    public float currentExp;
    public float hpStats;
    public float attackStats;
    public float moveSpeedStats;
    public List<int> unlockedPerks;      // �رݵ� Ư�� ID
    public Dictionary<itemSlotType, int> equippedItemDictionary;    // ������ ���� ������ ID ����Ʈ (ex: ����, ��, ��ų ��)

    public ClassData(Dictionary<itemSlotType, int> newEquipData, int level)
    {
        this.level = level;
        equippedItemDictionary = newEquipData;
    }
}
