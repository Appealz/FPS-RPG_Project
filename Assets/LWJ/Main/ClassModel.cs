using UnityEngine;

public class ClassModel
{
    public string SelectedClass { get; private set; }
    private string[] classList = { "rifler", "shotgunner", "engineer", "survivor" };
    private int index = 0;

    public ClassModel()
    {
        SelectedClass = classList[index];
    }

    public void ChangeClass(int dir)
    {
        index = (index + dir + classList.Length) % classList.Length;
        SelectedClass = classList[index];
    }
}