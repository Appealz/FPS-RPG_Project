using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPlayerInvenBlock : MonoBehaviour
{
    private TextMeshProUGUI weaponCurAmmoText;
    private TextMeshProUGUI refillPriceText;
    private TextMeshProUGUI fullRefillPriceText;
    private Button ammoRefillBtn;
    private Button ammoFullRefillBtn;

    private ShopItemBlock block;

    public void BlockInit(int index, Action<int> onBlockEvent, Action<AmmoRefillType, int> onAmmoRefillEvent)
    {
        if (TryGetComponent<ShopItemBlock>(out block))
        {
            block.InitBlock(index, onBlockEvent);
        }
        else Debug.Log($"{gameObject.name}_ShopPlayerInvenBlock.cs - BlockInit() - Can't Find ShopItemBlock");

        if (!MyUtility.GetChildrenTrans(transform, "WeaponCurAmmoText").TryGetComponent<TextMeshProUGUI>(out weaponCurAmmoText))
        {
            Debug.Log($"{gameObject.name}_ShopPlayerInvenBlock.cs - BlockInit() - Can't Find WeaponCurAmmoText");
        }

        if (!MyUtility.GetChildrenTrans(transform, "AmmoRefillBtn").TryGetComponent<Button>(out ammoRefillBtn))
        {
            Debug.Log($"{gameObject.name}_ShopPlayerInvenBlock.cs - BlockInit() - Can't Find AmmoRefillBtn");
        }
        else ammoRefillBtn.onClick.AddListener(() => onAmmoRefillEvent.Invoke(AmmoRefillType.Normal, index));

        if (!MyUtility.GetChildrenTrans(ammoRefillBtn.transform, "RefillPriceText").TryGetComponent<TextMeshProUGUI>(out refillPriceText))
        {
            Debug.Log($"{gameObject.name}_ShopPlayerInvenBlock.cs - BlockInit() - Can't Find RefillPriceText");
        }

        if (!MyUtility.GetChildrenTrans(transform, "AmmoFullRefillBtn").TryGetComponent<Button>(out ammoFullRefillBtn))
        {
            Debug.Log($"{gameObject.name}_ShopPlayerInvenBlock.cs - BlockInit() - Can't Find AmmoFullRefillBtn");
        }
        else ammoFullRefillBtn.onClick.AddListener(() => onAmmoRefillEvent.Invoke(AmmoRefillType.Max, index));

        if (!MyUtility.GetChildrenTrans(ammoFullRefillBtn.transform, "FullRefillPriceText").TryGetComponent<TextMeshProUGUI>(out fullRefillPriceText))
        {
            Debug.Log($"{gameObject.name}_ShopPlayerInvenBlock.cs - BlockInit() - Can't Find FullRefillPriceText");
        }
    }

    public void BlockUpdate(IItem newItem)
    {
        block.BlockUpdate(newItem);
        if (newItem == null)
        {
            return;
        }

        weaponCurAmmoText.text = $"{newItem.GetItemCurrentData().currentMagazine} / {newItem.GetItemCurrentData().maxAmmo}";
        refillPriceText.text = "100";
        fullRefillPriceText.text = $"{(newItem.GetItemCurrentData().maxAmmo - newItem.GetItemCurrentData().currentMagazine) * 100}";
    }
}
