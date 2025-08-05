using UnityEngine;

public class ShopHealkitCtrl
{
    private Healkit curPlayerHealkit;
    public int healkitPrice { get; private set; }
    public int maxBuyPrice
    {
        get
        {
            if(curPlayerHealkit == null)
            {
                return 0;
            }

            int buyCount = curPlayerHealkit.GetItemCurrentData().maxAmmo - curPlayerHealkit.GetItemCurrentData().currentMagazine;
            return healkitPrice * buyCount;
        }
    }

    public void Init()
    {
        EventBus_InvenData.Publish(new InvenDataEvent((query) =>
        {
            foreach(var item in query)
            {
                if (item is Healkit healkit)
                    curPlayerHealkit = healkit;
            }

            if (curPlayerHealkit == null)
            {
                Debug.Log("ShopHealkitCtrl.cs - Init() - curPlayerHealkit can't Reference");
                return;
            }
            else
                healkitPrice = curPlayerHealkit.GetItemCurrentData().price;
        }));
        
    }

    public void BuyHealkit()
    {
        // todo Count++
        curPlayerHealkit.GetItemCurrentData().currentMagazine++;
        EventBus_HealkitUIUpdateEvent.Publish(new HealkitUIUpdateEvent());
    }

    /// <summary>
    /// 최대치까지 구매
    /// </summary>
    public void BuyHealKitFull()
    {
        curPlayerHealkit.healAmount = curPlayerHealkit.GetItemCurrentData().maxAmmo;
        EventBus_HealkitUIUpdateEvent.Publish(new HealkitUIUpdateEvent());
    }

    public HealkitPriceQueryData GetHealkitPrice()
    {
        return new HealkitPriceQueryData(healkitPrice, maxBuyPrice);
    }
}
