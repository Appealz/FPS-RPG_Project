using System;
using System.Collections.Generic;
using UnityEngine;

public class InvenDataEvent
{
    public Action<List<IItem>> onInvenItemListEvent;

    public InvenDataEvent(Action<List<IItem>> onInvenItemListEvent)
    {
        this.onInvenItemListEvent = onInvenItemListEvent;
    }
}


public static class EventBus_InvenData
{
    public static void Publish(InvenDataEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<InvenDataEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<InvenDataEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}
