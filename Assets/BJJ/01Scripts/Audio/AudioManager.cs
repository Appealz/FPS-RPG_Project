using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AddressableAssets.Build;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManager : DontDestroySingleton<AudioManager>
{
    private AudioSetting AudioSetting;
    private UIAudioManager uiManager;
    private BGMAudioManager bgmManager;
    private PlayAudioManager playAudioManager;

    [SerializeField] private AudioSourcesObject audioSourcesObject;
    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup bgmMixer;
    [SerializeField] private AudioMixerGroup sfxMixer;

    private bool isInit = false;
    public bool IsInit => isInit;

    public async void InitAudioManager(AudioSetting setting)
    {
        if (isInit) return;

        isInit = true;
        // todo) 세팅 데이터 가져오면 세팅 데이터에서 AudioSetting을 참조해옴
        AudioSetting = setting;

        await MixerInit();
        VolumeChageHandler();
        uiManager = new UIAudioManager(sfxMixer);
        bgmManager = new BGMAudioManager(bgmMixer);
        playAudioManager = new PlayAudioManager(sfxMixer);

        AudioSetting.OnChanged += VolumeChageHandler;
    }

    private async UniTask MixerInit()
    {
        audioMixer = await Addressables.LoadAssetAsync<AudioMixer>("Audio/AudioMixer.mixer");
        var bgmMixers = audioMixer.FindMatchingGroups("BGM");
        bgmMixer = bgmMixers.FirstOrDefault();
        var sfxMixers = audioMixer.FindMatchingGroups("SFX");
        sfxMixer = sfxMixers.FirstOrDefault();
        audioSourcesObject = await Addressables.LoadAssetAsync<AudioSourcesObject>("Audio/AudioSources.asset");

        foreach (var sources in audioSourcesObject.AudioList)
        {
            audioClipDictionary.Add(sources.name, sources);
        }
    }


    private void OnDisable()
    {
        AudioSetting.OnChanged -= VolumeChageHandler;
    }

    public void UISFXPlay(string uisfx)
    {
        if(audioClipDictionary.TryGetValue(uisfx, out var clip))
        {
            uiManager.PlaySFX(clip);
        }
    }

    public void PlaySFXPlay(string sfx, Vector3? pos = null, bool spatial = true)
    {
        if(audioClipDictionary.TryGetValue(sfx, out var clip))
        {
            playAudioManager.PlaySFX(clip, pos, spatial);
        }
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
