using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemBlock : MonoBehaviour
{
    private TextMeshProUGUI weaponNameText;
    private TextMeshProUGUI weaponTypeText;
    private TextMeshProUGUI weaponLevelText;
    private TextMeshProUGUI weaponDamageText;
    private TextMeshProUGUI weaponAttackSpeedText;
    private TextMeshProUGUI weaponMaxAmmoText;
    private TextMeshProUGUI weaponPriceText;

    private GameObject blockContent;

    private Button blockBtn;

    public void InitBlock(int blockIndex, Action<int> onBlockBtn)
    {
        if(!MyUtility.GetChildrenTrans(transform, "WeaponNameText").TryGetComponent<TextMeshProUGUI>(out weaponNameText))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - WeaponName Can't Find");
        }
        if (!MyUtility.GetChildrenTrans(transform, "WeaponTypeText").TryGetComponent<TextMeshProUGUI>(out weaponTypeText))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - WeaponTypeText Can't Find");
        }
        if (!MyUtility.GetChildrenTrans(transform, "WeaponLevelText").TryGetComponent<TextMeshProUGUI>(out weaponLevelText))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - WeaponLevelText Can't Find");
        }
        if (!MyUtility.GetChildrenTrans(transform, "WeaponDamageText").TryGetComponent<TextMeshProUGUI>(out weaponDamageText))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - WeaponDamageText Can't Find");
        }
        if (!MyUtility.GetChildrenTrans(transform, "WeaponAttackSpeedText").TryGetComponent<TextMeshProUGUI>(out weaponAttackSpeedText))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - WeaponAttackSpeedText Can't Find");
        }
        if (!MyUtility.GetChildrenTrans(transform, "WeaponMaxAmmoText").TryGetComponent<TextMeshProUGUI>(out weaponMaxAmmoText))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - WeaponMaxAmmoText Can't Find");
        }
        if (!MyUtility.GetChildrenTrans(transform, "WeaponPriceText").TryGetComponent<TextMeshProUGUI>(out weaponPriceText))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - WeaponPriceText Can't Find");
        }
        if (!MyUtility.GetChildrenTrans(transform, "BlockBtn").TryGetComponent<Button>(out blockBtn))
        {
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - BlockBtn Can't Find");
        }
        else
            blockBtn.onClick.AddListener(() => onBlockBtn.Invoke(blockIndex));

        blockContent = MyUtility.GetChildrenTrans(transform, "BlockContent").gameObject;
        if(blockContent == null)
            Debug.Log($"{gameObject.name}_ShopItemBlock.cs - InitBlock() - BlockContent Can't Find");
    }

    public void BlockUpdate(IItem newItem)
    {
        if(newItem == null)
        {
            blockContent.SetActive(false);
            return;
        }

        if (!blockContent.activeSelf)
            blockContent.SetActive(true);

        var data = newItem.GetItemCurrentData();
        weaponNameText.text = data.name;
        weaponLevelText.text = data.level.ToString();
        weaponDamageText.text = data.damage.ToString();
        weaponAttackSpeedText.text = ASTransToRPM(data.firerRate).ToString();
        weaponMaxAmmoText.text = data.maxAmmo.ToString();
        weaponPriceText.text = data.price.ToString();
    }

    /// <summary>
    /// 무기의 공격속도를 분당발사속도로 변환하는 함수
    /// </summary>
    /// <param name="attackSpeed"> 분당 발사 속도입니다.</param>
    /// <returns></returns>
    private int ASTransToRPM(float attackSpeed)
    {
        float temp = 60f / attackSpeed;
        return Mathf.RoundToInt(temp);
    }
}
