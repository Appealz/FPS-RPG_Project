using System;
using UnityEngine;

public class PlayerHPQuery
{
    public Func<int> GetPlayerCurHP;
    public Func<int> GetPlayerMaxHP;
}

public class EventBus_PlayerHPQuery
{
    public static void Subscribe(Action<PlayerHPQuery> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<PlayerHPQuery> newMethod) => EventBus.UnSubscribe(newMethod);
    public static void Publish(PlayerHPQuery evt) => EventBus.Publish(evt);
}

public class PlayerHPChangeEvent
{
    public int curHP;
    public int maxHP;
    public int curArmor;
    public int maxArmor;

    public PlayerHPChangeEvent(int curHP, int maxHP, int curArmor, int maxArmor)
    {
        this.curHP = curHP;
        this.maxHP = maxHP;
        this.curArmor = curArmor;
        this.maxArmor = maxArmor;
    }
}
public class EventBus_PlayerHPChangeEvent
{
    public static void Subscribe(Action<PlayerHPChangeEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<PlayerHPChangeEvent> newMethod) => EventBus.UnSubscribe(newMethod);
    public static void Publish(PlayerHPChangeEvent evt) => EventBus.Publish(evt);
}

public class PlayerArmorChangeEvent
{
    public int curArmor;
    public int maxArmor;

    public PlayerArmorChangeEvent(int curArmor, int maxArmor)
    {
        this.curArmor = curArmor;
        this.maxArmor = maxArmor;
    }
}

public class EventBus_PlayerArmorChangeEvent
{
    public static void Subscribe(Action<PlayerArmorChangeEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<PlayerArmorChangeEvent> newMethod) => EventBus.UnSubscribe(newMethod);
    public static void Publish(PlayerArmorChangeEvent evt) => EventBus.Publish(evt);
}