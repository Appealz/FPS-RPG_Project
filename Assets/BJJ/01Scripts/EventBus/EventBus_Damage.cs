using System;
using UnityEngine;

public enum DamageType
{
    Damage,
    Heal
}

public class DamageInfo
{
    public GameObject sender;
    public GameObject receiver;
    public DamageType type;
    public float damage;
    public Buff buff;

    public DamageInfo (GameObject newSender, GameObject newReceiver, float newDamage, Buff newBuff, DamageType type)
    {
        sender = newSender;
        receiver = newReceiver;
        damage = newDamage;
        buff = newBuff;
        this.type = type;
    }
}

public class DamageResult
{
    public GameObject sender;
    public GameObject receiver;
    public float damage;
    public DamageReceivePart part;

    public DamageResult (GameObject newSender, GameObject newReceiver, float newDamage, DamageReceivePart newPart)
    {
        sender = newSender;
        receiver = newReceiver;
        damage = newDamage;
        part = newPart;
    }
}

public static class EventBus_Damage
{
    public static void SubScribe(Action<DamageInfo> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<DamageInfo> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(DamageInfo damageInfo)
    {
        EventBus.Publish(damageInfo);
    }
}

public static class EventBus_DamageResult
{
    public static void SubScribe(Action<DamageResult> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubScribe(Action<DamageResult> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(DamageResult result)
    {
        EventBus.Publish(result);
    }
}