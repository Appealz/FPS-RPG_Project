using System.Collections.Generic;
using UnityEngine;

public class ShopManager : DestroySingleton<ShopManager>
{
    [SerializeField] private ShopItemCtrl _shopItemCtrl;
    [SerializeField] private ShopArmorCtrl _armorCtrl;
    [SerializeField] private ShopHealkitCtrl _healkitCtrl;

    public void InitShop()
    {
        _shopItemCtrl = new ShopItemCtrl();
    }

    // �������� ��� �Ĵ� �ż����
    private void BuyItemHandler(int index)
    {
        if(_shopItemCtrl.SelectItem(index, out IItem buyItem))
        {
            // todo)
            // EventBus_Item.Publish(AddItem)
            // EventBus_Currency.Publish(RemoveMoney)
            return;
        }

        Debug.Log($"ShopManager.cs - BuyItemHandler() - {index} is Can't Buy Item");
    }

    private void SellItemHandler(int index)
    {
        // todo
        // PlayerInventory << Remove Item EventBus
        // EventBus_Currency.Publish(AddMoney)
    }

    private void ShopUIHandler(bool isOn)
    {
        // todo EventBus UI
        // ���� UI ���� �ݴ� �ż���
    }
}
