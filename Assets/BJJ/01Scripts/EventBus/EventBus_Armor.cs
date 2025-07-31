using System;
using UnityEngine;

public class ArmorChangeEvent
{
    public IItem newArmor;

    public ArmorChangeEvent(IItem newArmor)
    {
        this.newArmor = newArmor;
    }
}


public static class EventBus_Armor
{
    public static void Publish(ArmorChangeEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ArmorChangeEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ArmorChangeEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}
