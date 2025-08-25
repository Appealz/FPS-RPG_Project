using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSourcesObject", menuName = "Scriptable Objects/Audio/AudioSourcesObject")]
public class AudioSourcesObject : ScriptableObject
{
    public List<AudioClip> AudioList;
}
