using UnityEngine;

public class ClassViewModel : ViewModelBase
{
    ClassModel model;

    public ClassViewModel(ClassModel newModel)
    {
        model = newModel;
        ContextManager.Instance.SetSelectClass(model.SelectedClass);
    }

    public string GetSelectedClassName()
    {
        return model.SelectedClass;
    }

    public void ChangeClass(int dir)
    {
        model.ChangeClass(dir);
        ContextManager.Instance.SetSelectClass(model.SelectedClass);

        OnPropertyChanged(nameof(GetSelectedClassName));
    }

    public ClassData GetSelectedClassData()
    {
        return ContextManager.Instance.GetSelectedClassData();
    }
}
