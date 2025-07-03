using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItemCtrl
{
    [SerializeField] private int curSelectMinLevel;
    [SerializeField] private List<IItem> selectedItemList;
    
    public void Init()
    {
        // todo Context -> minItemLevel : Start Equip MainWeapon
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
}
