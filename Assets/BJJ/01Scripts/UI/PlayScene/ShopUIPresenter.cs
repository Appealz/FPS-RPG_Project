
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
        EventBus_ShopItemUpdate.Subscribe(ShopItemUpdate);
        shopUI.OnWeaponBuyEvent += WeaponBuyHandler;
        shopUI.OnWeaponSellEvent += WeaponSellHandler;
        shopUI.OnWeaponAmmoRefillEvent += WeaponAmmoRefillHandler;
    }

    public void DisableUI()
    {
        EventBus_ShopIsOn.UnSubscribe(UIOnOff);
        EventBus_ShopItemUpdate.UnSubscribe(ShopItemUpdate);
        shopUI.OnWeaponBuyEvent -= WeaponBuyHandler;
        shopUI.OnWeaponSellEvent -= WeaponSellHandler;
        shopUI.OnWeaponAmmoRefillEvent -= WeaponAmmoRefillHandler;
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

    private void ShopItemUpdate(ShopItemUpdateEvent evt)
    {
        shopUI.ShopUpdate(evt.shopItemList, evt.playerInven);
    }
}
