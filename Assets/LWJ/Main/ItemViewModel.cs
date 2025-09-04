using System.Threading.Tasks;
using UnityEngine;

public class ItemViewModel : ViewModelBase
{
    public ItemModel model;
    private Sprite selectItem;
    public Sprite SelectItem
    {
        get => selectItem;
        set
        {
            if(selectItem != value)
            {
                selectItem = value;
                OnPropertyChanged(nameof(SelectItem));
            }
        }
    }

    public ItemViewModel(ItemModel newModel)
    {
        model = newModel;
    }

    public async Task ShowItem(itemSlotType slot)
    {
        model.SelectSlot(slot);
        //SelectItem = await ResourceManager.Instance.LoadToSprite(model.SelectedItemID);
        Debug.Log($"Å×½ºÆ®{model.SlotType} {ContextManager.Instance.playClassName}");
    }
}