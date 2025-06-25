using System;
using UnityEngine;

public static class EventBus_EnemyManager
{
    public static void Subscribe(Action<EnemyUpdateEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<EnemyUpdateEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(EnemyUpdateEvent updateEvent)
    {
        EventBus.Publish(updateEvent);
    }
}
