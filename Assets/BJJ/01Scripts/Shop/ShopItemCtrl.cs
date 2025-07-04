using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItemCtrl
{
    [SerializeField] private List<IItem> selectedItemList; // �Ź� ������ ������ �����ϰ� �������� �̾Ƽ� ������ �̾� ����Ʈ
    [SerializeField] private IReadOnlyList<IItem> playerCurItems;
    
    public void Init()
    {
        // PlayerInventory ����
    }

    public void UpdateList()
    {
        // todo Random ����Ʈ �����
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
