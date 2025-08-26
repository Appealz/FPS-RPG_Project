using System;
using UnityEngine;

public class LobbyManager : DestroySingleton<LobbyManager>
{
    private MarketManager marketManager;

    protected override void DoAwake()
    {
        base.DoAwake();

        // todo 계정 매니저에 접근해서
        // 계정을 로드 했는지 판단 => 로드 안되어 있으면 로드
        // 로드 했으면 패스

        if(!SettingManager.Instance.IsInit) SettingManager.Instance.SettingInit();
    }

    private void Start()
    {
        // todo 여러 매니저들을 여기서 시작함
        AchievementManager.AchieveMentManagerInit();
        marketManager = new MarketManager();
        // todo UI 를 여기서 시작함
    }
}
