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
    public List<int> equippedItemIds;    // ������ ���� ������ ID ����Ʈ (ex: ����, ��, ��ų ��)
}
