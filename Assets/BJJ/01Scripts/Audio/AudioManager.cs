using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : DontDestroySingleton<AudioManager>
{
    private AudioSetting AudioSetting;
    private UIAudioManager uiManager;
    private BGMAudioManager bgmManager;
    private PlayAudioManager playAudioManager;

    private AudioMixer audioMixer;
    private AudioMixerGroup bgmMixer;
    private AudioMixerGroup sfxMixer;

    public void InitAudioManager(AudioSetting setting)
    {
        // todo) 세팅 데이터 가져오면 세팅 데이터에서 AudioSetting을 참조해옴
        AudioSetting = setting;

        uiManager = new UIAudioManager(sfxMixer);
        bgmManager = new BGMAudioManager(bgmMixer);
        playAudioManager = new PlayAudioManager(sfxMixer);

        AudioSetting.OnChanged += VolumeChageHandler;
    }

    private void OnDisable()
    {
        AudioSetting.OnChanged -= VolumeChageHandler;
    }

    public void UISFXPlay(string uisfx)
    {
        // todo 딕셔너리 형식으로 음원을 관리하여서
        // 스트링 -> 음원 -> 플레이 형식으로 
    }

    public void PlaySFXPlay(string sfx, Vector3? pos = null, bool spatial = true)
    {
        // todo UI와 동일
    }

    private void VolumeChageHandler()
    {
        audioMixer.SetFloat("Master", LinearToDecibel(AudioSetting.MasterVolume));
        audioMixer.SetFloat("BGM", LinearToDecibel(AudioSetting.BGMVolume));
        audioMixer.SetFloat("SFX", LinearToDecibel(AudioSetting.SFXVolume));
    }

    private float LinearToDecibel(float value)
    {
        if (value <= 0.0001f) return -80f;
        return 20f * Mathf.Log10(value);
    }
}
