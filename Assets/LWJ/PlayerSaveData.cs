using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    // ��ȭ, �����۸���Ʈ, ����, ������ ������(Ŭ����(������ ����, ����, Ư��, ĳ������ ������ ��������������))
    public float currency;
    // �����۸���Ʈ�� �رݿ���
    // public Dictionary<int, bool> unlockedItems;
    public int achievementID;
    public Dictionary<string, ClassData> classDatas;
}

// Save Load �޾ƿ���
// 