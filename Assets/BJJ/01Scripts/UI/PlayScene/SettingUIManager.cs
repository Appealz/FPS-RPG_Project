using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface ISettingUIManager
{
    event Action<SettingUIType,float> OnSettingDataChangeEvent;
}

public enum SettingUIType
{
    Master,
    BGM,
    SFX,
    Mouse
}

public class SettingUIManager : ISettingUIManager
{
    private Transform canvas;

    private Slider masterSlider;
    private TMP_InputField masterInput;
    private Slider bgmSlider;
    private TMP_InputField bgmInput;
    private Slider sfxSlider;
    private TMP_InputField sfxInput;
    private Slider mouseSlider;
    private TMP_InputField mouseInput;

    public event Action<SettingUIType, float> OnSettingDataChangeEvent;

    public SettingUIManager(Transform newCanvas)
    {
        canvas = newCanvas;

        // 슬라이더
        if (!MyUtility.GetChildrenTrans(canvas, "MasterSlider").TryGetComponent<Slider>(out masterSlider))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't Find MasterValumeSlider");
        else
        {
            masterSlider.value = SettingManager.Instance.SettingData.AudioSetting.MasterVolume;
            masterSlider.onValueChanged.AddListener((value) => SliderValueChange(SettingUIType.Master, value));
        }

        if (!MyUtility.GetChildrenTrans(canvas, "BGMSlider").TryGetComponent<Slider>(out bgmSlider))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't Find BGMValumeSlider");
        else
        {
            bgmSlider.value = SettingManager.Instance.SettingData.AudioSetting.BGMVolume;
            bgmSlider.onValueChanged.AddListener((value) => SliderValueChange(SettingUIType.BGM, value));
        }
        if (!MyUtility.GetChildrenTrans(canvas, "SFXSlider").TryGetComponent<Slider>(out sfxSlider))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't Find SFXValumeSlider");
        else
        {
            sfxSlider.value = SettingManager.Instance.SettingData.AudioSetting.SFXVolume;
            sfxSlider.onValueChanged.AddListener((value) => SliderValueChange(SettingUIType.SFX, value));
        }

        if (!MyUtility.GetChildrenTrans(canvas, "MouseSlider").TryGetComponent<Slider>(out mouseSlider))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't Find MouseSensitiveSlider");
        else
        {
            mouseSlider.value = SettingManager.Instance.SettingData.MouseSensitive;
            mouseSlider.onValueChanged.AddListener((value) => SliderValueChange(SettingUIType.Mouse, value));
        }

        // 인풋 필드
        if (!MyUtility.GetChildrenTrans(canvas, "MasterTextInput").TryGetComponent<TMP_InputField>(out masterInput))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't Find MasterValumeInputField");
        else
        {
            masterInput.text = Mathf.RoundToInt(SettingManager.Instance.SettingData.AudioSetting.MasterVolume * 100).ToString();
            masterInput.onEndEdit.AddListener((value) => InputFieldChange(SettingUIType.Master, value));
        }

        if (!MyUtility.GetChildrenTrans(canvas, "BGMTextInput").TryGetComponent<TMP_InputField>(out bgmInput))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't Find BGMValumeInputField");
        else
        {
            bgmInput.text = Mathf.RoundToInt(SettingManager.Instance.SettingData.AudioSetting.BGMVolume * 100).ToString();
            bgmInput.onEndEdit.AddListener((value) => InputFieldChange(SettingUIType.BGM, value));
        }

        if (!MyUtility.GetChildrenTrans(canvas, "SFXTextInput").TryGetComponent<TMP_InputField>(out sfxInput))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't Find SFXValumeInputField");
        else
        {
            sfxInput.text = Mathf.RoundToInt(SettingManager.Instance.SettingData.AudioSetting.SFXVolume * 100).ToString();
            sfxInput.onEndEdit.AddListener((value) => InputFieldChange(SettingUIType.SFX, value));
        }

        if (!MyUtility.GetChildrenTrans(canvas, "MouseTextInput").TryGetComponent<TMP_InputField>(out mouseInput))
            Debug.Log("SettingUIManager.cs - PlayScene_Can't MouseInputField");
        else
        {
            mouseInput.text = SettingManager.Instance.SettingData.MouseSensitive.ToString();
            mouseInput.onEndEdit.AddListener((value) => InputFieldChange(SettingUIType.Mouse, value));
        }
    }

    public void SettingUISetActive(bool isOn)
    {
        canvas.gameObject.SetActive(isOn);
    }

    private void SliderValueChange(SettingUIType type, float value)
    {
        switch (type)
        {
            case SettingUIType.Master:
                masterInput.text = Mathf.RoundToInt(value * 100).ToString();
                break;
            case SettingUIType.BGM:
                bgmInput.text = Mathf.RoundToInt(value * 100).ToString();
                break;
            case SettingUIType.SFX:
                sfxInput.text = Mathf.RoundToInt(value * 100).ToString();
                break;
            case SettingUIType.Mouse:
                mouseInput.text = value.ToString();
                break;
        }
        OnSettingDataChangeEvent?.Invoke(type, value);
    }

    private void InputFieldChange(SettingUIType type, string input)
    {
        if(!float.TryParse(input, out float value))
        {
            Debug.Log("SettingUIManager.cs - InputFieldChange() - Can't Parse Float Text");
            return;
        }

        switch (type)
        {
            case SettingUIType.Master:
                masterSlider.value = value / 100f;
                break;
            case SettingUIType.BGM:
                bgmSlider.value = value / 100f;
                break;
            case SettingUIType.SFX:
                sfxSlider.value = value / 100f;
                break;
            case SettingUIType.Mouse:
                mouseInput.text = value.ToString();
                break;
        }
        OnSettingDataChangeEvent?.Invoke(type, value);
    }
}
