using System;
using UnityEngine;

public class PauseEvent
{
    public bool isOn;

    public PauseEvent(bool isOn)
    {
        this.isOn = isOn;
    }
}

public static class EventBus_Pause
{
    public static void Subscribe(Action<PauseEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<PauseEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(PauseEvent evt)
    {
        EventBus.Publish(evt);
    }
}
