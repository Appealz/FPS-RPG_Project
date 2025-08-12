using System;
using UnityEngine;

[Serializable]
public class AchievementData
{
    public int enemyKill;
    
}

public static class AchievementManager
{
    private static AchievementData playerData;

    public static void AchieveMentManagerInit()
    {
        // todo 플레이어 계정에서 AchievementData를 가져옴
    }

    public static void LobbyStart()
    {
        // todo 로비씬 이벤트 버스 등록
    }

    public static void LobbyEnd()
    {
        // todo 로비씬 이벤트 버스 해제
    }

    public static void PlayStart()
    {
        // 플레이씬 이벤트 버스 등록
    }

    public static void PlayEnd()

    {
        // 플레이씬 이벤트 버스 해제
    }
}
