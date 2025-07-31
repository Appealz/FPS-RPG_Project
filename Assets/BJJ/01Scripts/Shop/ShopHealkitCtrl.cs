using UnityEngine;

public class ShopHealkitCtrl
{
    private Healkit curPlayerHealkit;
    public int healkitPrice { get; private set; }
    public int maxBuyPrice
    {
        get
        {
            int buyCount = curPlayerHealkit.GetItemCurrentData().maxAmmo - curPlayerHealkit.GetItemCurrentData().currentMagazine;
            return healkitPrice * buyCount;
        }
    }

    public void Init()
    {
        // todo 플레이어 인벤토리의 힐킷칸 연결
        // 플레이어의 특전 등에 의해서 가격이 변동되는걸 여기서 반영해서 캐싱해둠
        healkitPrice = curPlayerHealkit.GetItemCurrentData().price;
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
