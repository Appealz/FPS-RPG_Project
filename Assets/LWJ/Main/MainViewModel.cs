using System.Threading.Tasks;
using UnityEngine;

public class MainViewModel : ViewModelBase
{
    public ClassViewModel classVM { get; }
    public ItemViewModel itemVM { get; }

    private bool isOpenItemPopUp;
    public bool IsOpenItemPopUp
    {
        get => isOpenItemPopUp;
        private set
        {
            if(isOpenItemPopUp != value)
            {
                isOpenItemPopUp = value;
                OnPropertyChanged(nameof(IsOpenItemPopUp));
            }
        }
    }

    private bool isOpenShopPopUp;
    public bool IsOpenShopPopUp
    {
        get => isOpenShopPopUp;
        private set
        {
            if (isOpenShopPopUp != value)
            {
                isOpenShopPopUp = value;
                OnPropertyChanged(nameof(IsOpenShopPopUp));
            }
        }
    }

    public MainViewModel()
    {
        ClassModel classModel = new ClassModel();
        classVM = new ClassViewModel(classModel);
        itemVM = new ItemViewModel(new ItemModel());
    }

    public void StartGame()
    {

    }

    public void OpenShop()
    {
        IsOpenShopPopUp = true;
    }

    public async Task OpenItem()
    {
        IsOpenItemPopUp = true;
        await itemVM.ShowItem(itemSlotType.Main);
        itemVM.UpdateOwnItems();
    }

    public void ClosePopUp()
    {
        IsOpenItemPopUp = false;
        IsOpenShopPopUp = false;
    }

    public void OpenPerk()
    {

    }
}
