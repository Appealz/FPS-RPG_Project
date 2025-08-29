using UnityEngine;

public class ItemModel
{
    public itemSlotType SlotType { get; private set; }
    public int SelectedItemID { get; private set; }    

    public void SelectSlot(itemSlotType slot)
    {
        SlotType = slot;
        SelectedItemID = ContextManager.Instance.GetSelectedClassData().GetEquippedItemID(slot);        
    }
    
}