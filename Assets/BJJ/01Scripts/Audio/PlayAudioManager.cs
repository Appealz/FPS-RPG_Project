using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class PlayAudioManager
{
    private AudioMixerGroup audioMixer;
    private Stack<AudioSource> stk = new Stack<AudioSource>();
    private Transform stkParent;


    public PlayAudioManager(AudioMixerGroup mixer)
    {
        audioMixer = mixer;
        stkParent = new GameObject("PlaySFXPool").transform;
        stkParent.transform.parent = AudioManager.Instance.transform;
        CreateSource();
    }

    private void CreateSource(int addSize = 10)
    {
        for(int i = 0; i < addSize; i++)
        {
            GameObject go = new GameObject("PlaySFXSource");
            go.transform.parent = stkParent;
            var source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.outputAudioMixerGroup = audioMixer;
            stk.Push(source);
        }
    }

    public void PlaySFX(AudioClip clip, Vector3? pos = null, bool spatial = true)
    {
        if(stk.Count == 0)
            CreateSource(2);

        var source = stk.Pop();
        source.clip = clip;
        source.spatialBlend = spatial ? 1f : 0f;

        if(pos.HasValue)
            source.transform.position = pos.Value;

        source.Play();
        ReturnToPoolAfterPlay(source);
    }

    private async void ReturnToPoolAfterPlay(AudioSource source)
    {
        while(source.isPlaying)
            await UniTask.Yield();
        stk.Push(source);
    }
}
