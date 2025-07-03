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
    private Dictionary<int, int> items = new();
    private IItem currentItem;
    private int currentIndex;

    public PlayerInventory(GameObject newOwner, Dictionary<int, int> newItemDictionary)
    {
        owner = newOwner;
        if (newItemDictionary == null)
        {
            items = new Dictionary<int, int>();
        }
        else
        {
            items = newItemDictionary;
        }
    }

    // 1,2,3,4,5 Ű�� ���ε�
    public void EquipItem(int index)
    {
        if (!items.ContainsKey(index)) 
            return;        
        Debug.Log($"{index}������ ����");
        //todo: �̺�Ʈ�� PlayerItemController�� Equip(items[index]) ȣ��;
        EventBus_Item.Publish(new ItemChangedEvent(null, owner, ItemEventType.equip, items[index]));
    }

    public void AddItem(int id)
    {   
        if (id >= 1001 && id <= 1015)
        {
            if (!items.ContainsKey((int)itemSlotType.Main))
            {
                items[(int)itemSlotType.Main] = id;
                Debug.Log($"Main ���Կ� ����: {id}");
            }
            else if (!items.ContainsKey((int)itemSlotType.Sub))
            {
                items[(int)itemSlotType.Sub] = id;
                Debug.Log($"Sub ���Կ� ����: {id}");
            }
            else
            {
                Debug.LogWarning("Main/Sub ������ ��� ���� á���ϴ�. ������ ȹ�� ����.");
            }
        }
        else if (id == 1016)
        {
            items[(int)itemSlotType.Revolver] = id;
        }
        else if (id == 1017)
        {
            items[(int)(itemSlotType.Knife)] = id;
        }
        else if (id == 3001)
        {
            items[(int)(itemSlotType.HealKit)] = id;
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
                findKey = pair.Key;
            }
        }

        if (findKey.HasValue)
        {
            items.Remove(findKey.Value);
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
