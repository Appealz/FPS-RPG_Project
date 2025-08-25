using UnityEngine;
using UnityEngine.Audio;

public class BGMAudioManager
{
    private AudioMixerGroup audioMixer;
    private AudioSource audioSource;
    
    public BGMAudioManager(AudioMixerGroup mixer)
    {
        audioMixer = mixer;
        var go = new GameObject("BGMAudio");
        audioSource = go.AddComponent<AudioSource>();
        go.transform.parent = AudioManager.Instance.transform;
        audioSource.outputAudioMixerGroup = audioMixer;
    }

    public void PlayBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void BGMStop()
    {
        audioSource.Stop();
    }
}
