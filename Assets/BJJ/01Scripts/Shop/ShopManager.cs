using System.Collections.Generic;
using UnityEngine;

public class ShopManager : DestroySingleton<ShopManager>
{
    private GameObject player;
    [SerializeField] private ShopItemCtrl _shopItemCtrl;
    [SerializeField] private ShopArmorCtrl _armorCtrl;
    [SerializeField] private ShopHealkitCtrl _healkitCtrl;

    public void InitShop()
    {
        _shopItemCtrl = new ShopItemCtrl();
        _armorCtrl = new ShopArmorCtrl();
        _healkitCtrl = new ShopHealkitCtrl();

        _shopItemCtrl.Init();
        _armorCtrl.Init();
        _healkitCtrl.Init();

        player = FindAnyObjectByType<Player>().gameObject;

        EventBus_ShopBuyWeapon.Subscribe(BuyItemHandler);
        EventBus_ShopSellWeapon.Subscribe(SellItemHandler);
        EventBus_ShopAmmoRefillEvent.Subscribe(BuyAmmoHandler);
        EventBus_ShopHealkitBuyEvent.Subscribe(BuyHealkitHandler);
        EventBus_HealkitPriceQueryEvent.Subscribe(HealkitPriceQueryHandler);
        EventBus_ArmorBuyEvent.Subscribe(ArmorBuyEventHandler);
    }

    private void OnDisable()
    {
        EventBus_ShopBuyWeapon.UnSubscribe(BuyItemHandler);
        EventBus_ShopSellWeapon.UnSubscribe(SellItemHandler);
        EventBus_ShopAmmoRefillEvent.UnSubscribe(BuyAmmoHandler);
        EventBus_ShopHealkitBuyEvent.UnSubscribe(BuyHealkitHandler);
        EventBus_HealkitPriceQueryEvent.UnSubscribe(HealkitPriceQueryHandler);
        EventBus_ArmorBuyEvent.UnSubscribe(ArmorBuyEventHandler);
    }

    public void ShopUpdate()
    {
        _shopItemCtrl.UpdateList();
        _armorCtrl.ArmorUpdate();
        EventBus_ArmorUIUpdateEvent.Publish(new ArmorUIUpdateEvent());
        EventBus_HealkitUIUpdateEvent.Publish(new HealkitUIUpdateEvent());
    }

    // 아이템을 사고 파는 매서드들
    private void BuyItemHandler(ShopBuyWeapon evt)
    {
        if(_shopItemCtrl.SelectItem(evt.index, out IItem buyItem))
        {
            EventBus_CurrencyCheck.Publish(new CurrencyCheckEvent(player, buyItem.GetItemCurrentData().price, (canbuy) =>
            {
                if(!canbuy)
                {
                    Debug.Log($"ShopManager.cs - BuyItemHandler() - {evt.index} is Can't Buy Item");
                    return;
                }

                EventBus_Item.Publish(new ItemChangedEvent(buyItem, player, ItemEventType.add, buyItem.itemID));
                EventBus_Currency.Publish(new CurrencyChangeEvent(player, CurrencyChangeEventType.Remove, buyItem.GetItemCurrentData().price));
                _shopItemCtrl.RemoveShopItem(evt.index);
            }));
            return;
        }
        Debug.Log($"ShopManager.cs - BuyItemHandler() - {evt.index} is Can't Select Item");
    }

    private void BuyAmmoHandler(ShopAmmoRefillEvent evt)
    {
        switch(evt.type)
        {
            case AmmoRefillType.Normal:
                BuyAmmo(evt.index);
                break;
            case AmmoRefillType.Max:
                BuyFullAmmo(evt.index);
                break;
        }
    }

    private void BuyAmmo(int index)
    {
        if(_shopItemCtrl.SelectPlayerItem(index, out IItem item))
        {
            // EventBus << Ammo Add
            // EventBus_Currency.Publish(RemoveMoney)
        }
    }
    private void BuyFullAmmo(int index)
    {
        if (_shopItemCtrl.SelectPlayerItem(index, out IItem item))
        {
            // EventBus << Ammo Add
            // EventBus_Currency.Publish(RemoveMoney)
        }
    }

    private void SellItemHandler(ShopSellWeapon evt)
    {
        if(_shopItemCtrl.SelectPlayerItem(evt.index, out IItem item))
        {
            EventBus_Item.Publish(new ItemChangedEvent(item, player, ItemEventType.remove, item.itemID));
            EventBus_Currency.Publish(new CurrencyChangeEvent(player, CurrencyChangeEventType.Add, item.GetItemCurrentData().price));
            // UI 이벤트 송신
        }else
            Debug.Log($"ShopManager.cs - SellItemHandler() - {evt.index} is Can't Select Item");
    }

    private void ArmorBuyEventHandler(ArmorBuyEvent evt)
    {
        switch (evt.type)
        {
            case ShopArmorBtnType.Buy:
                BuyArmor(evt.index); break;
            case ShopArmorBtnType.Repair:
                RepairArmor();
                break;
        }
    }

    private void BuyArmor(int index)
    {
        EventBus_CurrencyCheck.Publish(new CurrencyCheckEvent(player, _armorCtrl.ArmorPrice(index), (buyCan) =>
        {
            if(!buyCan)
            {
                Debug.Log($"ShopManager.cs - SellItemHandler() - {index} is Can't Buy");
                return;
            }

            _armorCtrl.BuyArmor(player,index);
        }));
    }

    private void RepairArmor()
    {
        // todo 플레이어가 장착중인 아머의 수리비를 가져와서 체크하고
        // 가능하다면 수리 매서드를 호출 시키고 (이벤트 버스로)
        // 비용 소모
    }

    private void BuyHealkitHandler(HealkitBuyEvent evt)
    {
        switch (evt.type)
        {
            case HealkitBuyType.Normal:
                BuyHealkit();
                break;
            case HealkitBuyType.Max:
                BuyHealKiyFull();
                break;
        }
    }

    private void BuyHealkit()
    {
        EventBus_CurrencyCheck.Publish(new CurrencyCheckEvent(player, _healkitCtrl.healkitPrice, (canBuy) =>
        {
            if(!canBuy)
            {
                Debug.Log("ShopManager.cs - BuyHealkit() - Can Buy Healkit");
                return;
            }

            _healkitCtrl.BuyHealkit();
        }));
    }

    private void BuyHealKiyFull()
    {
        // 최대치까지 계산한 다음에
        // 힐킷 하나 구매랑 비슷하게 작동
        // 다만 BuyHealKitFull()로 처리
        EventBus_CurrencyCheck.Publish(new CurrencyCheckEvent(player, _healkitCtrl.maxBuyPrice, (canBuy) =>
        {
            if (!canBuy)
            {
                Debug.Log("ShopManager.cs - BuyHealkit() - Can MaxBuy Healkit");
                return;
            }
            _healkitCtrl.BuyHealKitFull();
        }));
    }

    private void HealkitPriceQueryHandler(HealkitPriceQueryEvent evt)
    {
        evt.onHealkitPriceQuery?.Invoke(_healkitCtrl.GetHealkitPrice());
    }
}
