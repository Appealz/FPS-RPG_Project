using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimEventData", menuName = "Scriptable Objects/AnimEventData")]
public class AnimEventData : ScriptableObject
{
    public string AnimName;
    public List<TimeEvent> EventList;
}

[System.Serializable]
public struct TimeEvent
{
    public float Time;
    public string EventName;
    public string Param;
}