using System;
using System.Collections.Generic;
using UnityEngine;

public class AchieveCheckEvent
{
    public AchievementType type;
    public int customID;
    public int value;

    public AchieveCheckEvent(AchievementType type, int value, int customID)
    {
        this.type = type;
        this.value = value;
        this.customID = customID;
    }
}

public static class AchievementManager
{
    // 업적 뷰 모델에서 playerData의 업적 리스트를 가져다 확인할 예정
    private static AchievementStat playerData;
    private static Dictionary<AchievementType, List<int>> achievementType = new Dictionary<AchievementType, List<int>>();

    public static void AchieveMentManagerInit()
    {
        // todo 플레이어 계정에서 AchievementStat를 가져옴
        // 테이블에서 업적 리스트에서
        // 모든 업적들의 타입별로 딕셔너리에 분류해서 넣어둠
        // 그 과정에서 테이블이 업데이트 됬다던가 하는걸 체크해서 새로 데이터를 생성해두기도 함
    }
    public static void PlayStart()
    {
        // 플레이씬 이벤트 버스 등록
        // 게임 진행 과정에서 수집해야하는 데이터들을 위주로 받는 이벤트를 등록할 예정
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
                if (playerData.enemyKill >= data.targetValue)
                    playerData.achievementData[id].isUnlocked = true;
                break;
            case AchievementType.ClearCount:
                if (playerData.clearCount >= data.targetValue)
                    playerData.achievementData[id].isUnlocked = true;
                break;
            case AchievementType.HealAmount:
                if (playerData.healAmount >= data.targetValue)
                    playerData.achievementData[id].isUnlocked = true;
                break;
            case AchievementType.CustomScript:
                // 추후에 생각해볼 예정
                break;
        }
    }

    #region PlayScene
    
    public static void AchieveCheckEventHandler(AchieveCheckEvent evt)
    {
        if(evt.type != AchievementType.CustomScript)
        {
            switch(evt.type)
            {
                case AchievementType.EnemyKill:
                    playerData.enemyKill += evt.value;
                    break;
                case AchievementType.ClearCount:
                    playerData.clearCount += evt.value;
                    break;
                case AchievementType.HealAmount:
                    playerData.healAmount += evt.value;
                    break;
                    // 클래스들 단위로도...
            }

            if (achievementType.TryGetValue(evt.type, out var list))
            {
                foreach (var i in list)
                {
                    AchieveCheck(i);
                }
            }
        }
        // 만약 업적에 따로 추가적으로 기록해야하는 경우 업적 세이브 데이터에 영향을 직접 줄 예정
    }

    #endregion

}
