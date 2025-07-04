using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum itemSlotType
{
    Main = 0,
    Sub,
    Revolver,
    Knife,
    HealKit
}

public class PlayerInventory
{
    private GameObject owner;
    //private List<IItem> items = new List<IItem>();
    private Dictionary<itemSlotType, int> items = new();
    private IItem currentItem;
    private int currentIndex;
    private Dictionary<itemSlotType, IItem> itemDictionary = new();

    public PlayerInventory(GameObject newOwner, Dictionary<itemSlotType, int> newItemDictionary)
    {
        owner = newOwner;
        if (newItemDictionary == null)
        {
            items = new Dictionary<itemSlotType, int>();
        }
        else
        {
            items = newItemDictionary;
            foreach(var item in newItemDictionary)
            {
                itemDictionary[item.Key] = WeaponManager.Instance.GetItemData(item.Value);
            }
        }
        
        Debug.Log("[PlayerInventory] 생성됨. 전달된 아이템 슬롯 수: " + items.Count);

        foreach (var pair in items)
        {
            Debug.Log($"[PlayerInventory] 슬롯: {pair.Key}, 아이템ID: {pair.Value}");
        }
    }

    // 1,2,3,4,5 키와 바인딩
    public void EquipItem(int index)
    {
        if (!items.ContainsKey((itemSlotType)index)) 
            return;
        Debug.Log($"{index}아이템 장착");
        //todo: 이벤트로 PlayerItemController에 Equip(items[index]) 호출;
        EventBus_Item.Publish(new ItemChangedEvent(null, owner, ItemEventType.equip, items[(itemSlotType)index]));
    }

    public void AddItem(int id)
    {   
        if (id >= 1001 && id <= 1015)
        {
            if (!items.ContainsKey(itemSlotType.Main))
            {
                items[(int)itemSlotType.Main] = id;
                Debug.Log($"Main 슬롯에 장착: {id}");
            }
            else if (!items.ContainsKey(itemSlotType.Sub))
            {
                items[itemSlotType.Sub] = id;
                Debug.Log($"Sub 슬롯에 장착: {id}");
            }
            else
            {
                Debug.LogWarning("Main/Sub 슬롯이 모두 가득 찼습니다. 아이템 획득 실패.");
            }
        }
        else if (id == 1016)
        {
            items[itemSlotType.Revolver] = id;
        }
        else if (id == 1017)
        {
            items[(itemSlotType.Knife)] = id;
        }
        else if (id == 3001)
        {
            items[(itemSlotType.HealKit)] = id;
        }
        else
            return;
            
    }

    public void RemoveItem(int removeItem)
    {
        int? findKey = null;
        foreach(var pair in items)
        {
            if (pair.Value == removeItem)
            {
                findKey = (int)pair.Key;
            }
        }

        if (findKey.HasValue)
        {
            items.Remove((itemSlotType)findKey.Value);
        }
        else
        {
            Debug.Log($"{removeItem} 아이템은 인벤토리에 없음");
        }
        // 리스트에서 현재 아이템 버리고
        // 다음 인덱스의 아이템을 꺼내오거나
        // 다음 인덱스의 아이템이 없는경우
        // 이전 인덱스의 아이템 착용
        // 권총, 칼은 버리기 x
    }

    public void SaveItemData()
    {

    }
}

public static class EventBus_Item
{
    public static void Subscribe(Action<ItemChangedEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }
    public static void UnSubscribe(Action<ItemChangedEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }
    public static void Publish(ItemChangedEvent type)
    {
        EventBus.Publish(type);
    }
}


public enum ItemEventType
{
    equip,
    remove,
    add,
}
public class ItemChangedEvent
{
    public IItem changeItem;
    public GameObject sender;
    public ItemEventType eventType;
    public int itemID;

    public ItemChangedEvent(IItem newItem, GameObject newSender, ItemEventType newEventType, int newItemID)
    {
        changeItem = newItem;
        sender = newSender;
        eventType = newEventType;
        itemID = newItemID;
    }
}
