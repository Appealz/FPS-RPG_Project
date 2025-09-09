using UnityEngine;

public enum LoadSceneType
{
    LobbyScene,
    PlayScene,
}

public static class SceneLoadManager
{
    private static string nextScene;

    public static void NextScene(LoadSceneType targetScene)
    {
        nextScene = targetScene.ToString();
    }

    public static string GetNextScene()
    {
        return nextScene;
    }
}
