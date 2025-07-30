using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopBuyWeapon
{
    public int index;
    public ShopBuyWeapon(int index)
    {
        this.index = index;
    }
}

public static class EventBus_ShopBuyWeapon
{
    public static void Publish(ShopBuyWeapon index) => EventBus.Publish(index);
    public static void Subscribe(Action<ShopBuyWeapon> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ShopBuyWeapon> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class ShopIsOnEvent
{
    public bool isOn;
    public ShopIsOnEvent(bool isOn) { this.isOn = isOn; }
}

public static class EventBus_ShopIsOn
{
    public static void Publish(ShopIsOnEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ShopIsOnEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ShopIsOnEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class ShopSellWeapon
{
    public int index;
    public ShopSellWeapon(int index)
    {
        this.index = index;
    }
}

public static class EventBus_ShopSellWeapon
{
    public static void Publish(ShopSellWeapon evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ShopSellWeapon> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ShopSellWeapon> newMethod) => EventBus.UnSubscribe(newMethod);
}

public enum AmmoRefillType
{ 
    Normal,
    Max,
}

public class ShopAmmoRefillEvent
{ 
    public AmmoRefillType type;
    public int index;

    public ShopAmmoRefillEvent(AmmoRefillType type, int index)
    {
        this.type = type;
        this.index = index;
    }
}

public static class EventBus_ShopAmmoRefillEvent
{
    public static void Publish(ShopAmmoRefillEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ShopAmmoRefillEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ShopAmmoRefillEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class ShopItemUpdateEvent
{
    public List<IItem> shopItemList;
    public List<IItem> playerInven;

    public ShopItemUpdateEvent(List<IItem> shopItemList, List<IItem> playerInven)
    {
        this.shopItemList = shopItemList;
        this.playerInven = playerInven;
    }
}

public static class EventBus_ShopItemUpdate
{
    public static void Publish(ShopItemUpdateEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ShopItemUpdateEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ShopItemUpdateEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public enum MedikitBuyType
{
    Normal,
    Max
}

