using UnityEngine;

public static class SaveLoadSystem
{
    public static PlayerSaveData Load()
    {
        return new PlayerSaveData();
    }

    public static void Save(PlayerSaveData saveData)
    {

    }
}