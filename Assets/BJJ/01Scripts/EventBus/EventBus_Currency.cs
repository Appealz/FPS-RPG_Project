using System;
using UnityEngine;

public enum CurrencyChangeEventType
{
    Add,
    Remove,
}

public class CurrencyChangeEvent
{
    public GameObject receiver;
    public CurrencyChangeEventType eventType;
    public int value;

    public CurrencyChangeEvent(GameObject newReceiver, CurrencyChangeEventType newType, int newValue)
    {
        receiver = newReceiver;
        eventType = newType;
        value = newValue;
    }
}

public class CurrencyQueryEvent
{
    public GameObject receiver;
    public Action<int> onResult;

    public CurrencyQueryEvent(GameObject newReceiver, Action<int> onResult)
    {
        receiver = newReceiver;
        this.onResult = onResult;
    }
}

public class CurrencyCheckEvent
{
    public GameObject receiver;
    public int price;
    public Action<bool> callback;

    public CurrencyCheckEvent(GameObject receiver, int newPrice,Action<bool> callback)
    {
        this.receiver = receiver;
        price = newPrice;
        this.callback = callback;
    }
}


public static class EventBus_Currency
{
    public static void Subscribe(Action<CurrencyChangeEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void Subscribe(Action<CurrencyCheckEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<CurrencyChangeEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(CurrencyChangeEvent evt)
    {
        EventBus.Publish(evt);
    }
}

public static class EventBus_CurrencyQuery
{
    public static void Subscribe(Action<CurrencyQueryEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<CurrencyQueryEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(CurrencyQueryEvent evt)
    {
        EventBus.Publish(evt);
    }
}

public static class EventBus_CurrencyCheck
{
    public static void Subscribe(Action<CurrencyCheckEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<CurrencyCheckEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(CurrencyCheckEvent evt)
    {
        EventBus.Publish(evt);
    }
}
