using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IShopUI
{
    event Action<int> OnWeaponBuyEvent;
    event Action<int> OnWeaponSellEvent;
    event Action<AmmoRefillType,int> OnWeaponAmmoRefillEvent;
    event Action<int> OnArmorBuyEvent;
    event Action<MedikitBuyType, int> OnMedikitBuyEvent;

    void Init(Transform canvas);

    void ShopOnOff(bool isOn);

    void ShopUpdate(List<IItem> showSellList, List<IItem> playerInven);
}

public class ShopUIManager : MonoBehaviour, IShopUI
{
    private Transform canvas;
    private List<ShopItemBlock> shopBuyBlocks = new List<ShopItemBlock>();
    private List<ShopPlayerInvenBlock> shopPlayerInvenBlocks = new List<ShopPlayerInvenBlock>();

    public event Action<int> OnWeaponBuyEvent;
    public event Action<int> OnWeaponSellEvent;
    public event Action<AmmoRefillType, int> OnWeaponAmmoRefillEvent;
    public event Action<int> OnArmorBuyEvent;
    public event Action<MedikitBuyType, int> OnMedikitBuyEvent;

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
                shopBuyBlocks[i].InitBlock(i, OnWeaponBuyEvent);
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
                shopPlayerInvenBlocks[i].BlockInit(i, OnWeaponSellEvent, OnWeaponAmmoRefillEvent);
            }
        }
    }

    public void ShopOnOff(bool isOn)
    {
        canvas.gameObject.SetActive(isOn);
    }

    public void ShopUpdate(List<IItem> showSellList, List<IItem> playerInven)
    {
        for(int i = 0; i < showSellList.Count; i++)
        {
            shopBuyBlocks[i].BlockUpdate(showSellList[i]);
        }

        for (int i = 0; i< playerInven.Count; i++)
        {
            shopPlayerInvenBlocks[i].BlockUpdate(playerInven[i]);
        }
    }
}
