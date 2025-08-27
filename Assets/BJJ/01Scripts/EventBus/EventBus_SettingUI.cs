using System;
using UnityEngine;

public class SettingUIEvent
{
    public SettingUIType type;
    public float value;

    public SettingUIEvent(SettingUIType type, float value)
    {
        this.type = type;
        this.value = value;
    }
}

public static class EventBus_SettingUI
{
    public static void Subscribe(Action<SettingUIEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<SettingUIEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(SettingUIEvent evt)
    {
        EventBus.Publish(evt);
    }
}

public class SettingIsOnEvent
{
    public bool isOn;

    public SettingIsOnEvent(bool isOn)
    {
        this.isOn = isOn;
    }
}

public static class EventBus_SettingIsOn
{
    public static void Subscribe(Action<SettingIsOnEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<SettingIsOnEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(SettingIsOnEvent evt)
    {
        EventBus.Publish(evt);
    }
}
