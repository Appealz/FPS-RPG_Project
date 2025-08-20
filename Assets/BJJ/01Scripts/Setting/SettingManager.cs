using UnityEngine;

public class SettingManager : DontDestroySingleton<SettingManager>
{
    private SettingData SettingData;
    public bool IsInit { get; private set; }

    // 키 세팅 매니저
    // 음량 매니저

    protected override void DoAwake()
    {
        IsInit = false;
    }

    /// <summary>
    /// 게임 시작시에 호출
    /// </summary>
    public void SettingInit()
    {
        if (IsInit) return;

        IsInit = true;
        // todo 세팅 데이터 호출 / 적용
        AudioManager.Instance.InitAudioManager(SettingData.AudioSetting);
    }

    public void LobbyStart()
    {
        // 로비씬용 이벤트 버스 등록
    }

    public void LobbyEnd()
    {
        // 로비씬용 이벤트 버스 해제
    }

    public void PlayStart()
    {
        // todo 인게임용 이벤트 버스 등록
    }

    public void PlayEnd()
    {
        // todo 인게임용 이벤트 버스 해제
    }
}
