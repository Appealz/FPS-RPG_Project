using System;
using UnityEngine;

public enum BuffEventType
{
    Add,
    Remove
}

public class BuffEvent
{
    public BuffEventType Type;
    public GameObject sender;
    public GameObject receiver;
    public Buff Buff;

    public BuffEvent(BuffEventType newType, GameObject newSender, GameObject newReceiver, Buff newBuff)
    {
        Type = newType;
        sender = newSender;
        receiver = newReceiver;
        Buff = newBuff;
    }
}


public static class EventBus_Buff
{
    public static void Subscribe(Action<BuffEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<BuffEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(BuffEvent evt)
    {
        EventBus.Publish(evt);
    }
}
