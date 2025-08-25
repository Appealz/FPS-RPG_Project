using UnityEngine;

public class TestAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSetting test = new AudioSetting();
        test.MasterVolume = 1.0f;
        test.SFXVolume = 0.5f;
        test.BGMVolume = 0.3f;

        AudioManager.Instance.InitAudioManager(test);
    }

}
