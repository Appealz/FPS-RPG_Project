using System;
using UnityEngine;
using UnityEngine.UI;

public interface IPauseUI
{
    event Action OnOptionBtnEvent;
    event Action OnExitBtnEvent;

    void PauseOnOff(bool isOn);
}

public class PauseManager : IPauseUI
{
    private Transform canvas;

    private Button continueBtn;
    private Button optionBtn;
    private Button exitBtn;

    public event Action OnOptionBtnEvent;
    public event Action OnExitBtnEvent;

    public PauseManager(Transform newCanvas)
    {
        canvas = newCanvas;

        var continueBtnObj = MyUtility.GetChildrenTrans(canvas, "ContinueBtn");
        if(continueBtnObj != null)
        {
            if (!continueBtnObj.TryGetComponent<Button>(out continueBtn))
                Debug.Log("PauseManager.cs - Can't Find ContinueBtn");
            else
                continueBtn.onClick.AddListener(ContinueBtnHandler);
        }

        var optionBtnObj = MyUtility.GetChildrenTrans(canvas, "OptionBtn");
        if(optionBtnObj != null)
        {
            if(!optionBtnObj.TryGetComponent<Button>(out optionBtn))
                Debug.Log("PauseManager.cs - Can't Find OptionBtn");
            else
                optionBtn.onClick.AddListener(OptionBtnHandler);
        }

        var exitBtnObj = MyUtility.GetChildrenTrans(canvas, "ExitBtn");
        if(exitBtnObj != null)
        {
            if(!optionBtnObj.TryGetComponent<Button>(out exitBtn))
                Debug.Log("PauseManager.cs - Can't Find ExitBtn");
            else
                exitBtn.onClick.AddListener(ExitBtnHandler);
        }

        PauseOnOff(false);
    }

    public void PauseOnOff(bool isOn)
    {
        canvas.gameObject.SetActive(isOn);
    }

    private void ContinueBtnHandler()
    {
        PauseOnOff(false);
        EventBus_Pause.Publish(new PauseEvent(false));
    }

    private void OptionBtnHandler()
    {
        OnOptionBtnEvent?.Invoke();
    }

    private void ExitBtnHandler()
    {
        OnExitBtnEvent?.Invoke();
    }
}
