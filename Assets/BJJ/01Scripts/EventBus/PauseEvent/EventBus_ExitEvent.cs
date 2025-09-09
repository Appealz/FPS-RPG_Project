using System;
using UnityEngine;

public class ExitEvent
{

}

public class EventBus_ExitEvent
{
    public static void Subscribe(Action<ExitEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<ExitEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(ExitEvent evt)
    {
        EventBus.Publish(evt);
    }
}
