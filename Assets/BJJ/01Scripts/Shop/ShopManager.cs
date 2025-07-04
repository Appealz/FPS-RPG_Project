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
    }

    public void ShopUpdate()
    {
        _shopItemCtrl.UpdateList();
    }

    // �������� ��� �Ĵ� �ż����
    public void BuyItemHandler(int index)
    {
        if(_shopItemCtrl.SelectItem(index, out IItem buyItem))
        {
            EventBus_CurrencyCheck.Publish(new CurrencyCheckEvent(player, 999, (canbuy) =>
            {
                if(!canbuy)
                {
                    Debug.Log($"ShopManager.cs - BuyItemHandler() - {index} is Can't Buy Item");
                    return;
                }

                EventBus_Item.Publish(new ItemChangedEvent(buyItem, player, ItemEventType.add, buyItem.itemID));
                EventBus_Currency.Publish(new CurrencyChangeEvent(player, CurrencyChangeEventType.Remove, 999));
            }));
            return;
        }
        Debug.Log($"ShopManager.cs - BuyItemHandler() - {index} is Can't Select Item");
    }

    public void BuyAmmo(int index)
    {
        if(_shopItemCtrl.SelectPlayerItem(index, out IItem item))
        {
            // EventBus << Ammo Add
            // EventBus_Currency.Publish(RemoveMoney)
        }
    }

    public void SellItemHandler(int index)
    {
        if(_shopItemCtrl.SelectPlayerItem(index, out IItem item))
        {
            EventBus_Item.Publish(new ItemChangedEvent(item, player, ItemEventType.remove, item.itemID));
            EventBus_Currency.Publish(new CurrencyChangeEvent(player, CurrencyChangeEventType.Add, 999));
            // UI �̺�Ʈ �۽�
        }
    }

    public void BuyArmor(int index)
    {

    }

    public void RepairArmor()
    {
        // todo �÷��̾ �������� �Ƹ��� ������ �����ͼ� üũ�ϰ�
        // �����ϴٸ� ���� �ż��带 ȣ�� ��Ű�� (�̺�Ʈ ������)
        // ��� �Ҹ�
    }

    public void BuyHealkit()
    {

    }

    public void BuyHealKiyFull()
    {
        // �ִ�ġ���� ����� ������
        // ��Ŷ �ϳ� ���Ŷ� ����ϰ� �۵�
        // �ٸ� BuyHealKitFull()�� ó��
    }
}
