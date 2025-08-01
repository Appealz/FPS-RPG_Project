using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShopArmorBtnType
{
    Buy,
    Repair
}


public class ShopArmorBlock : MonoBehaviour
{
    private ArmorData_Entity data;
    private TextMeshProUGUI armorNameText;
    private TextMeshProUGUI armorDurabilityText;
    private TextMeshProUGUI armorPrice;
    private Button blockBtn;
    private TextMeshProUGUI btnText;
    private ShopArmorBtnType type;

    public void BlockInit(int index, Action<ShopArmorBtnType ,int> onArmorBuyEvent)
    {
        int armorId = 2001 + index;

        if(!DataManager.Instance.GetArmorData(armorId, out data))
        {
            Debug.Log($"{gameObject.name}_ShopArmorBlock.cs - BlockInit() - Error ID {armorId}");
            return;
        }

        if (!MyUtility.GetChildrenTrans(transform, "ArmorNameText").TryGetComponent<TextMeshProUGUI>(out armorNameText))
        {
            Debug.Log($"{gameObject.name}_ShopArmorBlock.cs - BlockInit() - ArmorNameText Can't Find");
        }
        else
            armorNameText.text = data.name;

        if (!MyUtility.GetChildrenTrans(transform, "ArmorDurabilityText").TryGetComponent<TextMeshProUGUI>(out armorDurabilityText))
        {
            Debug.Log($"{gameObject.name}_ShopArmorBlock.cs - BlockInit() - ArmorDurabilityText Can't Find");
        }
        else
            armorDurabilityText.gameObject.SetActive(false);

        if (!MyUtility.GetChildrenTrans(transform, "ArmorPrice").TryGetComponent<TextMeshProUGUI>(out armorPrice))
        {
            Debug.Log($"{gameObject.name}_ShopArmorBlock.cs - BlockInit() - ArmorDurabilityText Can't Find");
        }
        else
            armorPrice.text = data.price.ToString();

        if (!MyUtility.GetChildrenTrans(transform, "BlockBtn").TryGetComponent<Button>(out blockBtn))
        {
            Debug.Log($"{gameObject.name}_ShopArmorBlock.cs - BlockInit() - ArmorDurabilityText Can't Find");
        }
        else
        {
            if (!MyUtility.GetChildrenTrans(blockBtn.transform, "BtnText").TryGetComponent<TextMeshProUGUI>(out btnText))
            {
                Debug.Log($"{gameObject.name}_ShopArmorBlock.cs - BlockInit() - BtnText Can't Find");
            }
            else
            {
                btnText.text = "구매";
            }

            type = ShopArmorBtnType.Buy;
            blockBtn.onClick.AddListener(() => onArmorBuyEvent?.Invoke(type,index));
        }

    }

    public void BlockUpdate()
    {
        EventBus_ArmorQueryEvent.Publish(new ArmorQueryEvent((evt) =>
        {
            if(evt.isEquipArmor && evt.curArmor.itemID == data.id)
            {
                type = ShopArmorBtnType.Repair;
                armorDurabilityText.gameObject.SetActive(true);
                armorDurabilityText.text = $"{evt.curDurability} / {data.durability}";
                btnText.text = "수리";
                // 가격 변동
                int price = Mathf.RoundToInt((evt.curDurability / data.durability) * data.price);
                armorPrice.text = $"{price}";
            }
            else
            {
                type = ShopArmorBtnType.Buy;
                if(armorDurabilityText.gameObject.activeSelf)
                    armorDurabilityText.gameObject.SetActive(false);

                armorPrice.text = data.price.ToString();
                btnText.text = "구매";
            }
        }));
    }
}
