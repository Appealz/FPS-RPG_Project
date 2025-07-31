using System.Collections.Generic;
using UnityEngine;

public class ShopArmorCtrl
{
    // todo DataManager << 방탄복 데이터 리스트를 가져와서
    // 보여줌
    private List<ArmorData_Entity> armorList = new List<ArmorData_Entity>();
    private IItem equipItem;

    public void Init()
    {
        //todo 데이터 매니저에서 리스트로 가져올 예정

    }

    public void BuyArmor(GameObject player, int index)
    {
        EventBus_Currency.Publish(new CurrencyChangeEvent(player, CurrencyChangeEventType.Remove, armorList[index].price));
        //EventBus_Armor.Publish(new ArmorChangeEvent());
    }

    public void RepairArmor()
    {
        //
    }

    public void ArmorUpdate()
    {
        EventBus_ArmorQueryEvent.Publish(new ArmorQueryEvent((evt) =>
        {
            if (evt.isEquipArmor)
            {
                equipItem = evt.curArmor;
            }
            else
            {
                equipItem = null;
            }
        }));
    }

    public int ArmorPrice(int index)
    {
        return armorList[index].price;
    }

    public int RepairPrice()
    {
        if (equipItem == null)
        {
            Debug.Log("ShopArmroCtrl.cs - RepairPrice() - NonEquip Armor");
            return -1;
        }
        DataManager.Instance.GetArmorData(equipItem.itemID, out var data);

        int price = Mathf.RoundToInt(equipItem.GetItemCurrentData().durability / data.durability * data.price);
        return price;
    }
}
