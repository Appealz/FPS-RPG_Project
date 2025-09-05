using System.Threading.Tasks;
using System.Windows.Input;
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

    private string itemName;
    public string ItemName
    {
        get => itemName;
        set
        {
            if(itemName != value)
            {
                itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
    }
        
    public string itemDamage { get; private set; }
    public string itemDescription {  get; private set; }

    public ItemViewModel(ItemModel newModel)
    {
        model = newModel;        
    }

    public async Task ShowItem(itemSlotType slot)
    {   
        model.SelectSlot(slot);
        int id = model.SelectedItemID;
        

        DataManager.Instance.GetItemData(id, out ItemData newItemData);

        if (newItemData is WeaponData weapon)
        {
            itemDamage = weapon.damagePerShot.ToString();
        }
        //itemDescription = newItemDat
        ItemName = newItemData.name;
        //SelectItem = await ResourceManager.Instance.LoadToSprite(model.SelectedItemID);
        Debug.Log($"Å×½ºÆ®{model.SlotType} {ContextManager.Instance.playClassName}");
    }

    public void ShowNextItem(int dir)
    {
        model.SelectItem(dir);
    }

    public void UpdateOwnItems()
    {
        var newClass = ContextManager.Instance.GetSelectedClassData();
        model.SetOwnItems(newClass.GetOwnItemsList());
    }
}