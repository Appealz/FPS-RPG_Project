using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IShopUI
{
    event Action<int> OnWeaponBuyEvent;
    event Action<int> OnWeaponSellEvent;
    event Action<AmmoRefillType,int> OnWeaponAmmoRefillEvent;
    event Action<ShopArmorBtnType, int> OnArmorBuyEvent;
    event Action<HealkitBuyType> OnHealkitBuyEvent;

    void Init(Transform canvas);

    void ShopOnOff(bool isOn);

    void ShopItemUpdate(List<IItem> showSellList, List<IItem> playerInven);
    void ArmorUpdate();
    void HealkitFullBuyPriceUpdate();
}

public class ShopUIManager : MonoBehaviour, IShopUI
{
    private Transform canvas;
    [SerializeField] private List<ShopItemBlock> shopBuyBlocks = new List<ShopItemBlock>();
    [SerializeField] private List<ShopPlayerInvenBlock> shopPlayerInvenBlocks = new List<ShopPlayerInvenBlock>();
    [SerializeField] private List<ShopArmorBlock> shopArmorBlocks = new List<ShopArmorBlock>();

    private TextMeshProUGUI healkitBuyPriceText;
    private TextMeshProUGUI healkitFullBuyPriceText;
    private TextMeshProUGUI curHealkitCountText;

    private Button healkitBuyBtn;
    private Button healkitFullBuyBtn;
    private Button shopUICloseBtn;

    public event Action<int> OnWeaponBuyEvent;
    public event Action<int> OnWeaponSellEvent;
    public event Action<AmmoRefillType, int> OnWeaponAmmoRefillEvent;
    public event Action<ShopArmorBtnType,int> OnArmorBuyEvent;
    public event Action<HealkitBuyType> OnHealkitBuyEvent;

    public void Init(Transform newCanvas)
    {
        canvas = newCanvas;

        var buyListRoot = MyUtility.GetChildrenTrans(canvas, "WeaponBuyList");
        if (buyListRoot == null)
            Debug.Log("ShopUIManager.cs - Init() - WeaponBuyList Non Reference");
        else
        {
            shopBuyBlocks = buyListRoot.GetComponentsInChildren<ShopItemBlock>().ToList();
            for (int i = 0; i < shopBuyBlocks.Count; i++)
            {
                shopBuyBlocks[i].InitBlock(i, OnWeaponBuyEventHandler);
            }
        }
        var playerInven = MyUtility.GetChildrenTrans(canvas, "PlayerInvenShow");
        if(playerInven == null)
            Debug.Log("ShopUIManager.cs - Init() - PlayerInvenShow Non Reference");
        else
        {
            shopPlayerInvenBlocks = playerInven.GetComponentsInChildren<ShopPlayerInvenBlock>().ToList();
            for(int i = 0; i < shopPlayerInvenBlocks.Count; i++)
            {
                shopPlayerInvenBlocks[i].BlockInit(i, OnWeaponSellEvent, OnWeaponAmmoRefillEventHandler);
            }
        }

        var armorUI = MyUtility.GetChildrenTrans(canvas, "ArmorUI");
        if(armorUI == null)
            Debug.Log("ShopUIManager.cs - Init() - ArmorUI Non Reference");
        else
        {
            shopArmorBlocks = armorUI.GetComponentsInChildren<ShopArmorBlock>().ToList();
            for(int i = 0; i < shopArmorBlocks.Count; i++)
            {
                shopArmorBlocks[i].BlockInit(i, OnArmorBuyEventHandler);
            }
        }

        if (!MyUtility.GetChildrenTrans(canvas, "HealkitBuyBtn").TryGetComponent<Button>(out healkitBuyBtn))
        {
            Debug.Log("ShopUIManager.cs - Init() - HealkitBuyBtn Non Reference");
        }
        else
            healkitBuyBtn.onClick.AddListener(() => OnHealkitBuyEvent?.Invoke(HealkitBuyType.Normal));
        if (!MyUtility.GetChildrenTrans(canvas, "HealkitFullBuyBtn").TryGetComponent<Button>(out healkitFullBuyBtn))
        {
            Debug.Log("ShopUIManager.cs - Init() - HealkitFullBuyBtn Non Reference");
        }
        else
            healkitFullBuyBtn.onClick.AddListener(() => OnHealkitBuyEvent?.Invoke(HealkitBuyType.Max));

        if(!MyUtility.GetChildrenTrans(canvas, "HealkitBuyPriceText").TryGetComponent<TextMeshProUGUI>(out healkitBuyPriceText))
            Debug.Log("ShopUIManager.cs - Init() - HealkitBuyPriceText Non Reference");
        else
        {
            EventBus_HealkitPriceQueryEvent.Publish(new HealkitPriceQueryEvent((query) =>
            {
                healkitBuyPriceText.text = $"{query.price}";
            }));
        }
        if (!MyUtility.GetChildrenTrans(canvas, "HealkitFullBuyPriceText").TryGetComponent<TextMeshProUGUI>(out healkitFullBuyPriceText))
            Debug.Log("ShopUIManager.cs - Init() - HealkitFullBuyPriceText Non Reference");

        if (!MyUtility.GetChildrenTrans(canvas, "CurHealkitCountText").TryGetComponent<TextMeshProUGUI>(out curHealkitCountText))
            Debug.Log("ShopUIManager.cs - Init() - CurHealkitCountText Non Reference");

        if (!MyUtility.GetChildrenTrans(canvas, "ShopCloseBtn").TryGetComponent<Button>(out shopUICloseBtn))
        {
            Debug.Log("ShopUIManager.cs - Init() - ShopCloseBtn Non Reference");
        }
        else
            shopUICloseBtn.onClick.AddListener(() => ShopOnOff(false));

        canvas.gameObject.SetActive(false);
    }

    public void ShopOnOff(bool isOn)
    {
        canvas.gameObject.SetActive(isOn);
        Cursor.visible = isOn;
        if(Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShopItemUpdate(List<IItem> showSellList, List<IItem> playerInven)
    {
        for(int i = 0; i < shopBuyBlocks.Count; i++)
        {
            shopBuyBlocks[i].BlockUpdate(showSellList[i]);
        }

        for (int i = 0; i< shopPlayerInvenBlocks.Count; i++)
        {
            shopPlayerInvenBlocks[i].BlockUpdate(playerInven[i]);
        }
    }

    public void HealkitFullBuyPriceUpdate()
    {
        EventBus_HealkitPriceQueryEvent.Publish(new HealkitPriceQueryEvent((query) =>
        {
            healkitFullBuyPriceText.text = $"{query.maxPrice}";
        }));
        EventBus_HealkitQueryEvent.Publish(new HealkitQueryEvent((query) =>
        {
            curHealkitCountText.text = $"{query.curCount} / {query.maxCount}";
        }));
    }

    private void OnWeaponBuyEventHandler(int index)
    {
        OnWeaponBuyEvent?.Invoke(index);
    }

    private void OnWeaponSellEventHandler(int index)
    {
        OnWeaponSellEvent?.Invoke(index);
    }

    private void OnWeaponAmmoRefillEventHandler(AmmoRefillType type, int index)
    {
        OnWeaponAmmoRefillEvent?.Invoke(type, index);
    }

    private void OnArmorBuyEventHandler(ShopArmorBtnType type, int index)
    {
        OnArmorBuyEvent?.Invoke(type, index);
    }

    public void ArmorUpdate()
    {
        foreach (var armorBlock in shopArmorBlocks)
        {
            armorBlock.BlockUpdate();
        }
    }
}
