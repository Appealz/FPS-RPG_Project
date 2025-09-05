using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel
{
    public itemSlotType SlotType { get; private set; }
    public int SelectedItemID { get; private set; }

    public List<int> ownItemList = new List<int>();

    private int currentIndex = 0;

    public void SelectSlot(itemSlotType slot)
    {
        SlotType = slot;
        SelectedItemID = ContextManager.Instance.GetSelectedClassData().GetEquippedItemID(slot);

        if(slot == itemSlotType.Main || slot == itemSlotType.Sub)
        {
            int index = ownItemList.IndexOf(SelectedItemID); // 현재 아이템의 인덱스 반환.
            currentIndex = (index >= 0) ? index : 0;
        }
    }

    public void SetOwnItems(List<int> newList)
    {
        ownItemList = newList;
    }

    public void SelectItem(int index)
    {
        currentIndex = (currentIndex + index + ownItemList.Count) % ownItemList.Count;
        SelectedItemID = ownItemList[currentIndex];
    }


}