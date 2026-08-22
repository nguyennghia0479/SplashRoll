using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("SFX Setting")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private float decibelMultiplier = 40f;

    [Header("Localization Setting")]
    [SerializeField] private TMP_Dropdown langDropdown;

    private const string SFX_PARAM = "sfxParam";
    private const string SELECTED_LOCALE = "SelectedLocale";
    private const string LOCALE_EN = "en";
    private const string LOCALE_VI = "vi-VN";

    private readonly float minSliderVal = .0001f;

    private void OnEnable()
    {
        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(OnSFXSlider);

        if (langDropdown != null)
            langDropdown.onValueChanged.AddListener(OnDropdownChange);

        LoadSettings();
    }

    private void OnDisable()
    {
        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(OnSFXSlider);

        if (langDropdown != null)
            langDropdown.onValueChanged.RemoveListener(OnDropdownChange);

        SaveSettings();
    }

    private void OnSFXSlider(float sliderValue)
    {
        float valueClamp = Mathf.Clamp(sliderValue, minSliderVal, 1f);
        float decibel = Mathf.Log10(valueClamp) * decibelMultiplier;
        audioMixer.SetFloat(SFX_PARAM, decibel);
    }

    private void OnDropdownChange(int selectValue)
    {
        if (selectValue == 0)
            UIEvents.RaiseLocaleChange(LOCALE_EN);
        else
            UIEvents.RaiseLocaleChange(LOCALE_VI);
    }

    private void SaveSettings()
    {
        SaveManager.SaveSFX(SFX_PARAM, sfxSlider.value);
        SaveManager.SaveLocale(SELECTED_LOCALE, langDropdown.value);
    }

    public void LoadSettings()
    {
        LoadSFXSetting();
        LoadLocaleSetting();
    }

    private void LoadSFXSetting()
    {
        float loadValue = SaveManager.LoadSFX(SFX_PARAM);
        sfxSlider.value = loadValue;
        OnSFXSlider(loadValue);
    }

    private void LoadLocaleSetting()
    {
        int loadValue = SaveManager.LoadLocale(SELECTED_LOCALE);
        langDropdown.value = loadValue;
        OnDropdownChange(loadValue);
    }
}
