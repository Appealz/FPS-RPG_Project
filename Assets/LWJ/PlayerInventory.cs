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
        
        Debug.Log("[PlayerInventory] ������. ���޵� ������ ���� ��: " + items.Count);

        foreach (var pair in items)
        {
            Debug.Log($"[PlayerInventory] ����: {pair.Key}, ������ID: {pair.Value}");
        }
    }

    // 1,2,3,4,5 Ű�� ���ε�
    public void EquipItem(int index)
    {
        if (!items.ContainsKey((itemSlotType)index)) 
            return;
        Debug.Log($"{index}������ ����");
        //todo: �̺�Ʈ�� PlayerItemController�� Equip(items[index]) ȣ��;
        EventBus_Item.Publish(new ItemChangedEvent(null, owner, ItemEventType.equip, items[(itemSlotType)index]));
    }

    public void AddItem(int id)
    {   
        if (id >= 1001 && id <= 1015)
        {
            if (!items.ContainsKey(itemSlotType.Main))
            {
                items[(int)itemSlotType.Main] = id;
                Debug.Log($"Main ���Կ� ����: {id}");
            }
            else if (!items.ContainsKey(itemSlotType.Sub))
            {
                items[itemSlotType.Sub] = id;
                Debug.Log($"Sub ���Կ� ����: {id}");
            }
            else
            {
                Debug.LogWarning("Main/Sub ������ ��� ���� á���ϴ�. ������ ȹ�� ����.");
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
            Debug.Log($"{removeItem} �������� �κ��丮�� ����");
        }
        // ����Ʈ���� ���� ������ ������
        // ���� �ε����� �������� �������ų�
        // ���� �ε����� �������� ���°��
        // ���� �ε����� ������ ����
        // ����, Į�� ������ x
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
