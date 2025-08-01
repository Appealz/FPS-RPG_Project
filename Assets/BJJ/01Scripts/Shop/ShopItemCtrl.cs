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
        // PlayerInventory 연결
    }

    public void UpdateList()
    {
        // todo Random 리스트 만들기
        selectedItemList = new List<IItem>();
        selectedItemList.Capacity = 3;
        playerCurItems = new List<IItem>();
        playerCurItems.Capacity = 2;

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
