using System;
using UnityEngine;

public static class EventBus_Stat
{
    public static void Subscribe(Action<StatModifier> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void Unsubscribe(Action<StatModifier> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(StatModifier modifer)
    {
        EventBus.Publish(modifer);
    }
}
