using System;
using UnityEngine;

public class UnitDieEvent
{
    public GameObject sender;

    public UnitDieEvent(GameObject sender)
    {
        this.sender = sender;
    }
}

public static class EventBus_UnitDieEvent
{
    public static void Subscribe(Action<UnitDieEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<UnitDieEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(UnitDieEvent dieEvent)
    {
        EventBus.Publish(dieEvent);
    }
}
