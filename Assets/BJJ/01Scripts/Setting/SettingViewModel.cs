using UnityEngine;

public enum VolumeType
{
    Master,
    BGM,
    SFX
}

public class SettingViewModel : ViewModelBase
{
    private SettingData settingData;

    public float MasterVolume
    {
        get => settingData.AudioSetting.MasterVolume;
        set
        {
            settingData.AudioSetting.MasterVolume = value;
            OnPropertyChanged(nameof(MasterVolume));
        }
    }

    public float BGMVolume
    {
        get => settingData.AudioSetting.BGMVolume;
        set
        {
            settingData.AudioSetting.BGMVolume = value;
            OnPropertyChanged(nameof(BGMVolume));
        }
    }

    public float SFXVolume
    {
        get => settingData.AudioSetting.SFXVolume;
        set
        {
            settingData.AudioSetting.SFXVolume = value;
            OnPropertyChanged(nameof(SFXVolume));
        }
    }

    public SettingViewModel(SettingData data)
    {
        settingData = data;
    }

    public void OnChangeVolume(VolumeType type, float value)
    {
        switch(type)
        {
            case VolumeType.Master:
                MasterVolume = value;
                break;
            case VolumeType.BGM:
                BGMVolume = value;
                break;
            case VolumeType.SFX:
                SFXVolume = value;
                break;
        }
    }
}
