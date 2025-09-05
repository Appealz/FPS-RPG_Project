using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClassData
{
    public int level;
    public float currentExp;
    public float hpStats;
    public float attackStats;
    public float moveSpeedStats;
    public string description;
    public List<int> unlockedPerks;      // 해금된 특전 ID
    public Dictionary<itemSlotType, int> equippedItemDictionary;    // 마지막 장착 아이템 ID 리스트 (ex: 무기, 방어구, 스킬 등)
    public List<int> equippedItems;
    public List<int> ownItems;

    public void InitData(BaseClassData newData)
    {
        level = 1;
        currentExp = 0f;
        hpStats = newData.maxHp;
        attackStats = newData.damage;
        moveSpeedStats = newData.moveSpeed;
        description = newData.description;

        // 기본 제공 아이템
        equippedItemDictionary = new Dictionary<itemSlotType, int>
        {
            { itemSlotType.Main, newData.baseMainWeaponID },
            { itemSlotType.Revolver, newData.baseRevolverID},
            { itemSlotType.Knife,newData.baseKnifeID},
        };

        ownItems = new List<int>
        {
            newData.baseMainWeaponID,
        };

        unlockedPerks = new List<int>();
    }

    // 아이템 장착 (메인씬에서 플레이할 아이템 선택시 슬롯의 아이템 변경)
    public void EquipItem(itemSlotType slotType, int itemId)
    {
        if(!ownItems.Contains(itemId))
        {
            Debug.Log("보유하지 않은 아이템을 장착하려하고 있습니다.");
            return;
        }
        equippedItemDictionary[slotType] = itemId;
    }

    // 현재 슬롯에 장착되어있는 아이템 아이디 리턴
    public int GetEquippedItemID(itemSlotType slotType)
    {
        if (equippedItemDictionary.TryGetValue(slotType, out int id))
            return id;

        return -1;
    }

    // 아이템 추가 메소드(아이템 구매시)
    public void AddItem(int itemID)
    {
        if (DataManager.Instance.GetItemData(itemID, out ItemData data))
        {
            equippedItems.Add(itemID);
        }
        else
        {
            Debug.LogWarning($"존재하지 않는 아이템 ID: {itemID}");
        }    
    }

    // 보유중인 아이템 리스트 리턴
    public List<int> GetOwnItemsList()
    {
        return ownItems;
    }

    public List<int> GetEquippedItems()
    {
        return equippedItems;
    }
}
