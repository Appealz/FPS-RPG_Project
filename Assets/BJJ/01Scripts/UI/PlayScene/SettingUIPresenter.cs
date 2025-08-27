using UnityEngine;
using UnityEngine.UI;

public class SettingUIPresenter
{
    private ISettingUIManager settingUI;

    public SettingUIPresenter(ISettingUIManager newSettingUI)
    {
        settingUI = newSettingUI;

        settingUI.OnSettingDataChangeEvent += SettingDataChange;
    }

    public void OnDisable()
    {
        settingUI.OnSettingDataChangeEvent -= SettingDataChange;
    }

    private void SettingDataChange(SettingUIType type, float value)
    {
        EventBus_SettingUI.Publish(new SettingUIEvent(type, value));
    }
}
