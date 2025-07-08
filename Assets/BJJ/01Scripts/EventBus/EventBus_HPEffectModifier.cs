using System;
using UnityEngine;

public class HPEffectModifier
{
    public GameObject sender;
    public GameObject receiver;
    public StatEventType eventType;
    public IHpEffectModifier effectModifier;

    public HPEffectModifier(GameObject sender, GameObject receiver, StatEventType eventType,IHpEffectModifier effectModifier)
    {
        this.sender = sender;
        this.receiver = receiver;
        this.eventType = eventType;
        this.effectModifier = effectModifier;
    }
}


public static class EventBus_HPEffectModifier
{
    public static void Subscribe(Action<HPEffectModifier> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<HPEffectModifier> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(HPEffectModifier effectModifier)
    {
        EventBus.Publish(effectModifier);
    }
}
