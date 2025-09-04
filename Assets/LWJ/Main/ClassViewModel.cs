using UnityEngine;

public class ClassViewModel : ViewModelBase
{
    ClassModel model;

    private string selectedClass;
    public string SelectedClass
    {
        get => selectedClass;
        private set
        {
            if (selectedClass != value)
            {
                selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass));
            }
        }
    }

    public ClassViewModel(ClassModel newModel)
    {
        model = newModel;
        SelectedClass = model.SelectedClass;
        ContextManager.Instance.SetSelectClass(model.SelectedClass);
    }

    public void ChangeClass(int dir)
    {
        model.ChangeClass(dir);        
        SelectedClass = model.SelectedClass;
        ContextManager.Instance.SetSelectClass(SelectedClass);
    }

    public ClassData GetSelectedClassData()
    {
        return ContextManager.Instance.GetSelectedClassData();
    }
}
