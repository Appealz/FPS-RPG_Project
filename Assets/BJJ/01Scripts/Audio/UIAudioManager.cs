using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEditor.PlayerSettings;

public class UIAudioManager
{
    private Stack<AudioSource> stk = new Stack<AudioSource>();
    private Transform stkParent;
    private AudioMixerGroup audioMixer;

    public UIAudioManager(AudioMixerGroup mixer)
    {
        audioMixer = mixer;
        stkParent = new GameObject("UISFXPool").transform;
    }

    private void CreateSource(int addSize = 10)
    {
        for(int i = 0; i < addSize; i++)
        {
            GameObject go = new GameObject("UISFX");
            go.transform.parent = stkParent;
            AudioSource source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.outputAudioMixerGroup = audioMixer;
            source.spatialBlend = 0;
            stk.Push(source);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (stk.Count == 0)
            CreateSource(2);

        var source = stk.Pop();
        source.clip = clip;

        source.Play();
        ReturnToPoolAfterPlay(source);
    }

    private async void ReturnToPoolAfterPlay(AudioSource source)
    {
        while (source.isPlaying)
            await UniTask.Yield();
        stk.Push(source);
    }
}
