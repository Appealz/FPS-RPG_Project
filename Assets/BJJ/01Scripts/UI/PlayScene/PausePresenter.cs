using UnityEngine;

public class PausePresenter
{
    private IPauseUI pauseUI;

    public PausePresenter(IPauseUI newUI)
    {
        pauseUI = newUI;

        pauseUI.OnOptionBtnEvent += SettingBtnEvent;
        pauseUI.OnExitBtnEvent += ExitBtnEvent;

        EventBus_Pause.Subscribe(PauseHandler);
    }

    private void SettingBtnEvent()
    {
        EventBus_SettingIsOn.Publish(new SettingIsOnEvent(true));
    }

    private void ExitBtnEvent()
    {
        EventBus_ExitEvent.Publish(new ExitEvent());
    }

    private void PauseHandler(PauseEvent evt)
    {
        if(evt.isOn)
            pauseUI.PauseOnOff(true);
    }

    public void OnDisable()
    {
        pauseUI.OnOptionBtnEvent -= SettingBtnEvent;
        pauseUI.OnExitBtnEvent -= ExitBtnEvent;
        EventBus_Pause.UnSubscribe(PauseHandler);
    }
}
