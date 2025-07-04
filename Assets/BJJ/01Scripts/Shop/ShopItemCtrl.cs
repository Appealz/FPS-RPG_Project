using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItemCtrl
{
    [SerializeField] private List<IItem> selectedItemList; // 매번 상점이 열릴때 랜덤하게 아이템을 뽑아서 가지고 이쓸 리스트
    [SerializeField] private IReadOnlyList<IItem> playerCurItems;
    
    public void Init()
    {
        // PlayerInventory 연결
    }

    public void UpdateList()
    {
        // todo Random 리스트 만들기
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
        selectedItemList.RemoveAt(index);
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
}
