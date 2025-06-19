using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory
{
    private GameObject owner;
    private List<IItem> items = new List<IItem>();
    private IItem currentItem;
    private int currentIndex;

    public PlayerInventory(GameObject newOwner, List<IItem> newItemList)
    {
        owner = newOwner;
        if (newItemList == null)
        {
            items = new List<IItem>();
        }
        else
        {
            items = newItemList;
        }
    }

    // 1,2,3,4,5 키와 바인딩
    public void EquipItem(int index)
    {
        Debug.Log($"{index}아이템 장착");
        // 이벤트로 PlayerItemController에 Equip(items[index]) 호출;
        EventBus_Item.Publish(new ItemChangedEvent(items[index], owner, ItemEventType.equip));
    }

    public void AddItem(IItem newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(IItem removeItem)
    {
        if(items.Contains(removeItem))
        {
            items.Remove(removeItem);
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
}

public static class EventBus_Item
{
    public static void Subscribe<ItemChangedEvent>(Action<ItemChangedEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }
    public static void UnSubscribe<ItemChangedEvent>(Action<ItemChangedEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }
    public static void Publish<ItemChangedEvent>(ItemChangedEvent type)
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
    public IItem equipItem;
    public GameObject sender;
    public ItemEventType eventType;

    public ItemChangedEvent(IItem newEquipItem, GameObject newSender, ItemEventType newEventType)
    {
        equipItem = newEquipItem;
        sender = newSender;
        eventType = newEventType;
    }
}
