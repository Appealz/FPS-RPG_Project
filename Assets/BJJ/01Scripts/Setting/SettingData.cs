using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingData
{
    public AudioSetting AudioSetting { get; set; }
    private float mouseSensitive;
    public float MouseSensitive
    {
        get { return mouseSensitive; }
        set { mouseSensitive = value; }
    }
}

[Serializable]
public class AudioSetting
{
    [NonSerialized]public Action OnChanged;

    [SerializeField] private float masterVolume;
    public float MasterVolume
    {
        get { return masterVolume; }
        set { masterVolume = value;  
            Mathf.Clamp01(masterVolume);
            OnChanged?.Invoke(); }
    }
    [SerializeField] private float sfxVolume;
    public float SFXVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = value;
            Mathf.Clamp(sfxVolume, 0, 1);
            OnChanged?.Invoke(); }
    }
    [SerializeField] private float bgmVolume;
    public float BGMVolume
    {
        get => bgmVolume;
        set { bgmVolume = value;
            Mathf.Clamp(bgmVolume, 0, 1f);
            OnChanged?.Invoke(); }
    }
}
