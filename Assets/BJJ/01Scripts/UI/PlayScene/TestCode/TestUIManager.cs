using UnityEngine;
using UnityEngine.InputSystem;

public class TestUIManager : DestroySingleton<TestUIManager>
{
    private Transform shopUICanvas;
    public Transform ShopUICanvas
    {
        get
        {
            if(shopUICanvas == null)
            {
                shopUICanvas = GameObject.Find("ShopCanvas").transform;
            }
            return shopUICanvas;
        }
    }
    private IShopUI shopUI;
    private ShopUIPresenter shopUIPresenter;

    private Transform settingCanvas;
    public Transform SettingCanvas
    {
        get
        {
            if (settingCanvas == null)
                settingCanvas = GameObject.Find("OptionCanvas").transform;
            return settingCanvas;
        }
    }

    private ISettingUIManager settingUI;
    private SettingUIPresenter settingUIPresenter;

    public void InitTestUI()
    {
        if (!TryGetComponent<IShopUI>(out shopUI))
            Debug.Log("TestUIManager.cs - InitTestUI() - IShopUI Can't Find");
        else
        {
            shopUI.Init(ShopUICanvas);
            shopUIPresenter = new ShopUIPresenter(shopUI);
        }

        settingUI = new SettingUIManager(SettingCanvas);
        settingUIPresenter = new SettingUIPresenter(settingUI);

        GameManager.OnGameUpdate += TestUpdate;
    }

    private void TestUpdate()
    {
        if(Keyboard.current.vKey.IsPressed())
        {
            EventBus_ShopIsOn.Publish(new ShopIsOnEvent(true));
            ShopManager.Instance.ShopUpdate();
        }
        if (Keyboard.current.escapeKey.IsPressed())
        {
            EventBus_SettingIsOn.Publish(new SettingIsOnEvent(true));
        }
    }

    private void OnDisable()
    {
        shopUIPresenter.DisableUI();
        settingUIPresenter.OnDisable();
        GameManager.OnGameUpdate -= TestUpdate;
    }
}
