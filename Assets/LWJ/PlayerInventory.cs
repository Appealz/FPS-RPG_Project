using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
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
    private Dictionary<itemSlotType, IItem> itemDictionary = new();

    private Transform weaponHolder;

    public PlayerInventory(GameObject newOwner, List<int> newItemList)
    {
        owner = newOwner;        
        foreach (var newItemID in newItemList)
        {
            if (newItemID == 1016 || newItemID == 1017)
            {
                InitWeaponLoad(newItemID).Forget();
            }
            else
            {
                AddItem(newItemID);
            }
        }
    }
    


    private async UniTaskVoid InitWeaponLoad(int itemID)
    {
        var prefab = await PrefabLoad.LoadToPrefab(itemID, PrefabType.Weapon);
        if(prefab == null)
        {
            Debug.Log($"존재하지 않는 프리팹 ID : {itemID}");
            return;
        }

        var weaponObj = GameObject.Instantiate(prefab);
        if(!weaponObj.TryGetComponent<IItem>(out var item))
        {
            Debug.Log("Iitem interface is not ref");
            return;
        }

        if (weaponHolder != null)
        {
            weaponObj.transform.SetParent(weaponHolder, false);
            weaponObj.transform.localPosition = Vector3.zero;
            weaponObj.transform.localRotation = Quaternion.identity;
            weaponObj.transform.localScale = Vector3.one;
            weaponObj.SetActive(false);
        }

        DataManager.Instance.GetWeaponData(itemID, out WeaponData_Entity saveWeaponData);
        WeaponData newItemData = new WeaponData(saveWeaponData);
        item.InitData(newItemData);

        itemDictionary[newItemData.slotType] = item;
    }

    public void SetHolder(Transform holder)
    {
        weaponHolder = holder;
    }

    // 1,2,3,4,5 키와 바인딩
    public void EquipItem(int index)
    {
        itemSlotType indexToSlot = (itemSlotType)index;
        if (!itemDictionary.TryGetValue((itemSlotType)index, out var item))
        {
            return;
        }           

        //if (!items.ContainsKey((itemSlotType)index)) 
        //    return;
        Debug.Log($"{index}아이템 장착");
        //todo: 이벤트로 PlayerItemController에 Equip(items[index]) 호출;
        EventBus_Item.Publish(new ItemChangedEvent(itemDictionary[indexToSlot], owner, ItemEventType.equip, itemDictionary[indexToSlot].itemID));
    }

    // 아이템 등록(획득)
    public void AddItem(int id)
    {
        var item = WeaponManager.Instance.GetItemInterface(id);
        var data = WeaponManager.Instance.GetItemData(id);

        if (item == null || data == null)
            return;

        item.InitData(data);

        itemSlotType baseSlot = itemSlotType.Main;

        if (data is WeaponData weaponData)
        {
            baseSlot = weaponData.slotType;
        }

        if(!itemDictionary.ContainsKey(baseSlot))
        {
            itemDictionary[baseSlot] = item;
            Debug.Log($"{baseSlot} 슬롯에 아이템 추가됨 : {id}");
            //EventBus_Item.Publish(new ItemChangedEvent(item, owner, ItemEventType.add, id));
        }
        else
        {
            Debug.LogWarning($"{baseSlot} 슬롯에 이미 아이템이 존재합니다.");
        }            
    }

    public void RemoveItem(int removeItem)
    {
        itemSlotType? targetSlot = null;

        foreach (var pair in itemDictionary)
        {
            if (pair.Value.itemID == removeItem)
            {
                targetSlot = pair.Key;
                break;
            }
        }

        if (targetSlot.HasValue)
        {
            var removedItem = itemDictionary[targetSlot.Value];
            itemDictionary.Remove(targetSlot.Value);
            Debug.Log($"{removedItem.itemID} 아이템 제거됨");
            EventBus_Item.Publish(new ItemChangedEvent(removedItem, owner, ItemEventType.remove, removedItem.itemID));
        }
        else
        {
            Debug.Log($"{removeItem} 아이템은 인벤토리에 없음");
        }

        //int? findKey = null;
        //foreach(var pair in items)
        //{
        //    if (pair.Value == removeItem)
        //    {
        //        findKey = (int)pair.Key;
        //    }
        //}

        //if (findKey.HasValue)
        //{
        //    items.Remove((itemSlotType)findKey.Value);
        //}
        //else
        //{
        //    Debug.Log($"{removeItem} 아이템은 인벤토리에 없음");
        //}

        // 리스트에서 현재 아이템 버리고
        // 다음 인덱스의 아이템을 꺼내오거나
        // 다음 인덱스의 아이템이 없는경우
        // 이전 인덱스의 아이템 착용
        // 권총, 칼은 버리기 x


    }

    public void SaveItemData()
    {

    }

    public List<IItem> GetEquippedItems()
    {
        return itemDictionary.Values.ToList();
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
