using UnityEngine;

public class SettingManager : DontDestroySingleton<SettingManager>
{
    private SettingData settingData;
    public SettingData SettingData => settingData;
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
        settingData = new SettingData();
        settingData.MouseSensitive = 0.1f;
        settingData.AudioSetting = new AudioSetting();
        settingData.AudioSetting.MasterVolume = 1.0f;
        settingData.AudioSetting.BGMVolume = 1.0f;
        settingData.AudioSetting.SFXVolume = 1.0f;
        AudioManager.Instance.InitAudioManager(settingData.AudioSetting);
    }

    public void PlayStart()
    {
        EventBus_SettingUI.Subscribe(SettingDataEventHandler);
    }

    public void PlayEnd()
    {
        EventBus_SettingUI.UnSubscribe(SettingDataEventHandler);
    }

    private void SettingDataEventHandler(SettingUIEvent evt)
    {
        switch(evt.type)
        {
            case SettingUIType.Master:
                settingData.AudioSetting.MasterVolume = evt.value;
                break;
            case SettingUIType.BGM:
                settingData.AudioSetting.BGMVolume = evt.value;
                break;
            case SettingUIType.SFX:
                settingData.AudioSetting.SFXVolume = evt.value;
                break;
            case SettingUIType.Mouse:
                settingData.MouseSensitive = evt.value;
                break;
        }
    }
}
