using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerHPUI
{
    void HPChange(int curHP, int maxHP, int curArmor, int maxArmor);
}

public class PlayerHPUIManager : IPlayerHPUI
{
    private Transform canvas;

    private Image hpBar;
    private TextMeshProUGUI hpText;
    
    private Image armorBar;
    private TextMeshProUGUI armorText;

    public PlayerHPUIManager(Transform newCanvas)
    {
        canvas = newCanvas;

        var hp = MyUtility.GetChildrenTrans(canvas, "HPBar");
        if (hp == null)
            Debug.Log("PlayerHPUIManager.cs - Can't Find HPBar");
        else
        {
            if (!hp.TryGetComponent<Image>(out hpBar))
                Debug.Log("PlayerHPUIManager.cs - Don't Reference Image");
            else
                hpBar.fillAmount = 1f;
        }

        var hpt = MyUtility.GetChildrenTrans(canvas, "HPText");
        if(hpt == null)
            Debug.Log("PlayerHPUIManager.cs - Can't Find HPBar");
        else
        {
            if (!hpt.TryGetComponent<TextMeshProUGUI>(out hpText))
                Debug.Log("PlayerHPUIManager.cs - Don't Reference TextMeshProUGUI");
            else
            {
                var query = new PlayerHPQuery();
                EventBus_PlayerHPQuery.Publish(query);
                float curHP = query.GetPlayerCurHP();
                float maxHP = query.GetPlayerMaxHP();
                hpText.text = curHP.ToString() + " / " + maxHP.ToString();
            }
        }

        var armor = MyUtility.GetChildrenTrans(canvas, "ArmorBar");
        if (armor == null)
            Debug.Log("PlayerHPUIManager.cs - Can't Find ArmorBar");
        else
        {
            if (!armor.TryGetComponent<Image>(out armorBar))
                Debug.Log("PlayerHPUIManager.cs - Don't Reference Image");
        }

        var armort = MyUtility.GetChildrenTrans(canvas, "ArmorText");
        if (armort == null)
            Debug.Log("PlayerHPUIManager.cs - Can't Find ArmorText");
        else
        {
            if (!armort.TryGetComponent<TextMeshProUGUI>(out armorText))
                Debug.Log("PlayerHPUIManager.cs - Don't Reference TextMeshProUGUI");
            else
            {
                EventBus_ArmorQueryEvent.Publish(new ArmorQueryEvent((evt) =>
                {
                    if (evt.isEquipArmor)
                    {
                        armorText.text = evt.curDurability.ToString("n1") + " / " + evt.curArmor.GetItemCurrentData().durability.ToString("n1");
                        armorBar.fillAmount = evt.curDurability / evt.curArmor.GetItemCurrentData().durability;
                    }
                    else
                    {
                        armorText.text = "0 / 0";
                        armorBar.fillAmount = 0;
                    }
                }));
            }
        }
    }

    public void HPChange(int curHP, int maxHP, int curArmor, int maxArmor)
    {
        hpBar.fillAmount = (float)curHP / (float)maxHP;
        hpText.text = curHP.ToString() + " / " + maxHP.ToString();
        armorBar.fillAmount = (float) curArmor / (float)maxArmor;
        armorText.text = curArmor.ToString() + " / " + maxArmor.ToString();
    }
}

public class PlayerHPUIPresenter
{
    private IPlayerHPUI playerHPUI;

    public PlayerHPUIPresenter(IPlayerHPUI newUI)
    {
        playerHPUI = newUI;

        EventBus_PlayerHPChangeEvent.Subscribe(PlayerHPChangeEventHandler);
    }

    private void PlayerHPChangeEventHandler(PlayerHPChangeEvent evt)
    {
        playerHPUI.HPChange(evt.curHP, evt.maxHP, evt.curArmor, evt.maxArmor);
    }

    public void OnDisable()
    {
        EventBus_PlayerHPChangeEvent.UnSubscribe(PlayerHPChangeEventHandler);
    }
}