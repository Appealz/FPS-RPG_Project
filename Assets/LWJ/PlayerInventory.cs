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
    private Dictionary<itemSlotType, int> items = new();
    private IItem currentItem;
    private int currentIndex;
    private Dictionary<itemSlotType, IItem> itemDictionary = new();
    private Dictionary<itemSlotType, ItemData> itemSlotData = new();

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

        //if (newItemDictionary == null)
        //{
        //    items = new Dictionary<itemSlotType, int>();
        //}
        //else
        //{
        //    items = newItemDictionary;
        //    foreach(var item in newItemDictionary)
        //    {
        //        itemSlotData[item.Key] = WeaponManager.Instance.GetItemData(item.Value);
        //    }
        //}
        
        //Debug.Log("[PlayerInventory] 생성됨. 전달된 아이템 슬롯 수: " + items.Count);

        //foreach (var pair in items)
        //{
        //    Debug.Log($"[PlayerInventory] 슬롯: {pair.Key}, 아이템ID: {pair.Value}");
        //}
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

        DataManager.Instance.GetWeaponData(itemID, out WeaponData_Entity saveWeaponData);
        WeaponData newItemData = new WeaponData(saveWeaponData);
        item.InitData(newItemData);

        itemDictionary[newItemData.slotType] = item;
    }

    // 1,2,3,4,5 키와 바인딩
    public void EquipItem(int index)
    {
        itemSlotType indexToSlot = (itemSlotType)index;
        if (!itemDictionary.TryGetValue((itemSlotType)index, out var item))
            return;

        //if (!items.ContainsKey((itemSlotType)index)) 
        //    return;
        Debug.Log($"{index}아이템 장착");
        //todo: 이벤트로 PlayerItemController에 Equip(items[index]) 호출;
        EventBus_Item.Publish(new ItemChangedEvent(itemDictionary[indexToSlot], owner, ItemEventType.equip, itemDictionary[indexToSlot].itemID));
    }

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

        //if (id >= 1001 && id <= 1015)
        //{
        //    if (!items.ContainsKey(itemSlotType.Main))
        //    {
        //        items[(int)itemSlotType.Main] = id;
        //        Debug.Log($"Main 슬롯에 장착: {id}");
        //    }
        //    else if (!items.ContainsKey(itemSlotType.Sub))
        //    {
        //        items[itemSlotType.Sub] = id;
        //        Debug.Log($"Sub 슬롯에 장착: {id}");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Main/Sub 슬롯이 모두 가득 찼습니다. 아이템 획득 실패.");
        //    }
        //}
        //else if (id == 1016)
        //{
        //    items[itemSlotType.Revolver] = id;
        //}
        //else if (id == 1017)
        //{
        //    items[(itemSlotType.Knife)] = id;
        //}
        //else if (id == 3001)
        //{
        //    items[(itemSlotType.HealKit)] = id;
        //}
        //else
        //    return;
            
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
