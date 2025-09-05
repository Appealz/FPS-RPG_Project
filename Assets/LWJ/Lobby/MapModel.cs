using UnityEngine;

public class MapModel
{
    public string SelectMap { get; private set; }
    private string[] mapList = { "Factory", "Ground" };
    private int currentIndex = 0;

    public MapModel()
    {
        SelectMap = mapList[currentIndex];
    }

    public void ChangeMap(int index)
    {
        currentIndex = (currentIndex + index + mapList.Length) % mapList.Length;
        SelectMap = mapList[currentIndex];
    }
}