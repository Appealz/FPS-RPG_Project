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

    // 1,2,3,4,5 Ű�� ���ε�
    public void EquipItem(int index)
    {
        if (index >= items.Count || index > 0)
            return;
        Debug.Log($"{index}������ ����");
        // todo: �̺�Ʈ�� PlayerItemController�� Equip(items[index]) ȣ��;
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

    public ItemChangedEvent(IItem newItem, GameObject newSender, ItemEventType newEventType)
    {
        changeItem = newItem;
        sender = newSender;
        eventType = newEventType;
    }
}
