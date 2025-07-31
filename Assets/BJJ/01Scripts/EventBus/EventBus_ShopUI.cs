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

public enum HealkitBuyType
{
    Normal,
    Max
}

public class HealkitBuyEvent
{
    public HealkitBuyType type;

    public HealkitBuyEvent(HealkitBuyType type)
    {
        this.type = type;
    }
}

public static class EventBus_ShopHealkitBuyEvent
{
    public static void Publish(HealkitBuyEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<HealkitBuyEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<HealkitBuyEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class ArmorBuyEvent
{
    public ShopArmorBtnType type;
    public int index;

    public ArmorBuyEvent(ShopArmorBtnType type, int index)
    {
        this.type = type;
        this.index = index;
    }
}

public static class EventBus_ArmorBuyEvent
{
    public static void Publish(ArmorBuyEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ArmorBuyEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ArmorBuyEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class HealkitQueryData
{
    public int curCount;
    public int maxCount;

    public HealkitQueryData(int curCount, int maxCount)
    {
        this.curCount = curCount;
        this.maxCount = maxCount;
    }
}

public class HealkitQueryEvent
{
    public Action<HealkitQueryData> onHealkitQueryEvent; 

    public HealkitQueryEvent(Action<HealkitQueryData> action)
    {
        onHealkitQueryEvent = action;
    }
}

public static class EventBus_HealkitQueryEvent
{
    public static void Publish(HealkitQueryEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<HealkitQueryEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<HealkitQueryEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class ArmorQueryEvent
{
    public Action<EquipArmorData> OnEquipArmorEvent;

    public ArmorQueryEvent(Action<EquipArmorData> onEquipArmorEvent)
    {
        OnEquipArmorEvent = onEquipArmorEvent;
    }
}

public static class EventBus_ArmorQueryEvent
{
    public static void Publish(ArmorQueryEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ArmorQueryEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ArmorQueryEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class HealkitUIUpdateEvent
{ }

public static class EventBus_HealkitUIUpdateEvent
{
    public static void Publish(HealkitUIUpdateEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<HealkitUIUpdateEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<HealkitUIUpdateEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class ArmorUIUpdateEvent { }

public static class EventBus_ArmorUIUpdateEvent
{
    public static void Publish(ArmorUIUpdateEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<ArmorUIUpdateEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<ArmorUIUpdateEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

public class HealkitPriceQueryData
{
    public int price;
    public int maxPrice;

    public HealkitPriceQueryData(int price, int maxPrice)
    {
        this.price = price;
        this.maxPrice = maxPrice;
    }
}

public class HealkitPriceQueryEvent
{
    public Action<HealkitPriceQueryData> onHealkitPriceQuery;
    public HealkitPriceQueryEvent(Action<HealkitPriceQueryData> onHealkitPriceQuery)
    {
        this.onHealkitPriceQuery = onHealkitPriceQuery;
    }
}
public static class EventBus_HealkitPriceQueryEvent
{
    public static void Publish(HealkitPriceQueryEvent evt) => EventBus.Publish(evt);
    public static void Subscribe(Action<HealkitPriceQueryEvent> newMethod) => EventBus.Subscribe(newMethod);
    public static void UnSubscribe(Action<HealkitPriceQueryEvent> newMethod) => EventBus.UnSubscribe(newMethod);
}

