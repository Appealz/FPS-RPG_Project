
using UnityEngine;

public class ShopUIIsOnEvent
{
    public bool isOn;
    public ShopUIIsOnEvent(bool isOn)
    {
        this.isOn = isOn;
    }
}

public class ShopUIPresenter
{
    private IShopUI shopUI;

    public ShopUIPresenter(IShopUI view)
    {
        shopUI = view;
        EventBus_ShopIsOn.Subscribe(UIOnOff);
        EventBus_ShopItemUpdate.Subscribe(ShopItemUpdateHandler);
        EventBus_ArmorUIUpdateEvent.Subscribe(ShopArmorUpdateHandler);
        EventBus_HealkitUIUpdateEvent.Subscribe(ShopHealkitUpdateHandler);
        shopUI.OnWeaponBuyEvent += WeaponBuyHandler;
        shopUI.OnWeaponSellEvent += WeaponSellHandler;
        shopUI.OnWeaponAmmoRefillEvent += WeaponAmmoRefillHandler;
        shopUI.OnHealkitBuyEvent += HealkitBuyHandler;
        shopUI.OnArmorBuyEvent += ArmorBuyBtnHandler;
    }

    public void DisableUI()
    {
        EventBus_ShopIsOn.UnSubscribe(UIOnOff);
        EventBus_ShopItemUpdate.UnSubscribe(ShopItemUpdateHandler);
        EventBus_HealkitUIUpdateEvent.UnSubscribe(ShopHealkitUpdateHandler);
        EventBus_ArmorUIUpdateEvent.UnSubscribe(ShopArmorUpdateHandler);
        shopUI.OnWeaponBuyEvent -= WeaponBuyHandler;
        shopUI.OnWeaponSellEvent -= WeaponSellHandler;
        shopUI.OnWeaponAmmoRefillEvent -= WeaponAmmoRefillHandler;
        shopUI.OnHealkitBuyEvent -= HealkitBuyHandler;
        shopUI.OnArmorBuyEvent -= ArmorBuyBtnHandler;
    }

    private void UIOnOff(ShopIsOnEvent evt)
    {
        shopUI.ShopOnOff(evt.isOn);
    }

    private void WeaponBuyHandler(int index)
    {
        EventBus_ShopBuyWeapon.Publish(new ShopBuyWeapon(index));
    }
    private void WeaponSellHandler(int index)
    {
        EventBus_ShopSellWeapon.Publish(new ShopSellWeapon(index));
    }

    private void WeaponAmmoRefillHandler(AmmoRefillType type, int index)
    {
        EventBus_ShopAmmoRefillEvent.Publish(new ShopAmmoRefillEvent(type, index));
    }

    private void HealkitBuyHandler(HealkitBuyType type)
    {
        EventBus_ShopHealkitBuyEvent.Publish(new HealkitBuyEvent(type));
    }

    private void ArmorBuyBtnHandler(ShopArmorBtnType type, int index)
    {
        EventBus_ArmorBuyEvent.Publish(new ArmorBuyEvent(type, index));
    }

    private void ShopItemUpdateHandler(ShopItemUpdateEvent evt)
    {
        shopUI.ShopItemUpdate(evt.shopItemList, evt.playerInven);
    }

    private void ShopHealkitUpdateHandler(HealkitUIUpdateEvent evt)
    {
        shopUI.HealkitFullBuyPriceUpdate();
    }

    private void ShopArmorUpdateHandler(ArmorUIUpdateEvent evt)
    {
        shopUI.ArmorUpdate();
    }
}
