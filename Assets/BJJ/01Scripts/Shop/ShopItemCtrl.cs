using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ShopItemCtrl
{
    [SerializeField] private List<IItem> selectedItemList; // 매번 상점이 열릴때 랜덤하게 아이템을 뽑아서 가지고 있을 리스트
    [SerializeField] private List<IItem> playerCurItems;
    
    public void Init()
    {
        selectedItemList = new List<IItem>();
        for (int i = 0; i < 3; i++)
        {
            selectedItemList.Add(null);
        }

        playerCurItems = new List<IItem>();
        for (int i = 0; i < 2; i++)
        {
            playerCurItems.Add(null);
        }
    }

    public void UpdateList()
    {
        // todo Random 리스트 만들기

        EventBus_InvenData.Publish(new InvenDataEvent((query) =>
        {
            for (int i = 0; i < query.Count; i++)
            {
                if(DataManager.Instance.GetWeaponData(query[i].itemID, out var data))
                {
                    if(data.weaponType == "Main")
                    {
                        playerCurItems[i] = query[i];
                    }
                }
            }
        }));

        EventBus_ShopItemUpdate.Publish(new ShopItemUpdateEvent(selectedItemList, playerCurItems.ToList()));
    }

    public bool SelectItem(int index, out IItem selectItem)
    {
        if(index < 0 || index >= selectedItemList.Count)
        {
            selectItem = null;
            return false;
        }

        IItem item = selectedItemList[index];
        selectItem = item;
        return true;
    }

    public bool SelectPlayerItem(int index, out IItem selectItem)
    {
        if (index < 0 || index >= playerCurItems.Count)
        {
            selectItem = null;
            return false;
        }

        IItem item = playerCurItems[index];
        selectItem = item;
        return true;
    }

    public void RemoveShopItem(int index)
    {
        if (selectedItemList[index] is MonoBehaviour mono)
        {
            WeaponManager.Instance.ReturnWeapon(mono.gameObject);
            selectedItemList[index] = null;
        }
        UpdateList();
    }

    public void AmmoRefill(int index)
    {
        playerCurItems[index].GetItemCurrentData().currentMagazine++;
    }

    public void AmmoFullRefill(int index)
    {
        playerCurItems[index].GetItemCurrentData().currentMagazine = playerCurItems[index].GetItemCurrentData().maxAmmo;
    }
}
