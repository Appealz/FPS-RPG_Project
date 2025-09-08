using UnityEngine;

public class UIManager : DestroySingleton<UIManager>
{
    private ShopUIManager shopUIManager;
    private ShopUIPresenter shopUIPresenter;

    private SettingUIManager settingUIManager;
    private SettingUIPresenter settingUIPresenter;

    public void InitPlayUI()
    {
        // 플레이어 관련 UI들 세팅 필요
        // 이후 해당 UI들을 연결하는 부분이 필요할듯 ㅇㅅㅇ;

        var shopCanvas = MyUtility.GetChildrenTrans(transform, "ShopCanvas");
        if (shopCanvas == null)
            Debug.Log("UIManager.cs - InitPlayUI() - ShopCanvas Can't Find");
        else
        {
            shopUIManager = new ShopUIManager(shopCanvas);
            shopUIPresenter = new ShopUIPresenter(shopUIManager);
        }

        var settingCanvas = MyUtility.GetChildrenTrans(transform, "OptionCanvas");
        if(settingCanvas == null)
            Debug.Log("UIManager.cs - InitPlayUI() - SettingCanvas Can't Find");
        else
        {
            settingUIManager = new SettingUIManager(settingCanvas);
            settingUIPresenter = new SettingUIPresenter(settingUIManager);
        }
    }

    private void OnDisable()
    {
        shopUIPresenter?.DisableUI();
        settingUIPresenter?.OnDisable();
    }
}
