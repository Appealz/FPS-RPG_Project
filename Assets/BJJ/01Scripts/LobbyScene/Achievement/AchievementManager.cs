using System;
using UnityEngine;


public static class AchievementManager
{
    private static AchievementStat playerData;

    public static void AchieveMentManagerInit()
    {
        // todo 플레이어 계정에서 AchievementStat를 가져옴
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

    /// <summary>
    /// 각 데이터 수집 매서드들이 작동하면 
    /// </summary>
    /// <param name="id"></param>
    private static void AchieveCheck(int id)
    {
        //업적 데이터쪽에서 해당 아이디로 가져와서 체크하기
        AchievementData data = new AchievementData();
        switch (data.achievementType)
        {
            case AchievementType.EnemyKill:
                if(playerData.enemyKill >= data.targetValue)
                    playerData.achievementData[id].isUnlocked = true;
                break;
            case AchievementType.ClearCount:
                if(playerData.clearCount >= data.targetValue)
                    playerData.achievementData[id].isUnlocked = true;
                break;
            case AchievementType.HealAmount:
                if(playerData.healAmount >= data.targetValue)
                    playerData.achievementData[id].isUnlocked = true;
                break;
            case AchievementType.CustomScript:
                // 추후에 생각해볼 예정
                break;
        }
    }
}
