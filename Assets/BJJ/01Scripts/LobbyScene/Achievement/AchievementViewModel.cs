using UnityEngine;

public class AchievementViewModel : ViewModelBase
{
    private AchievementStat playerData;

    public AchievementViewModel(AchievementStat playerData)
    {
        this.playerData = playerData;
    }


}
