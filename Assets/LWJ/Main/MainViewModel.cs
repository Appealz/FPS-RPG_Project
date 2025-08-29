using UnityEngine;

public class MainViewModel : ViewModelBase
{
    public ClassViewModel classVM { get; }

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

    }

    public void OpenItem()
    {

    }

    public void OpenPerk()
    {

    }
}
