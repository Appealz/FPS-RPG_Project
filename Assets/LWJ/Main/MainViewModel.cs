using UnityEngine;

public class MainViewModel : ViewModelBase
{
    public ClassViewModel classVM { get; }

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
    }

    public void StartGame()
    {

    }

    public void OpenShop()
    {
        IsOpenShopPopUp = true;
    }

    public void OpenItem()
    {
        IsOpenItemPopUp = true;
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
