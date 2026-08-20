using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("SFX Setting")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxParam = "sfxParam";
    [SerializeField] private float decibelMultiplier = 40f;

    [Header("Localization Setting")]
    [SerializeField] private TMP_Dropdown langDropdown;
    private const string LOCALE_EN = "en";
    private const string LOCALE_VI = "vi-VN";

    private readonly float minSliderVal = .0001f;

    private void OnEnable()
    {
        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(HandleSFXSlider);

        if (langDropdown != null)
            langDropdown.onValueChanged.AddListener(HandleDropdownChange);
    }

    private void OnDisable()
    {
        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(HandleSFXSlider);

        if (langDropdown != null)
            langDropdown.onValueChanged.RemoveListener(HandleDropdownChange);
    }

    private void HandleSFXSlider(float sliderValue)
    {
        float valueClamp = Mathf.Clamp(sliderValue, minSliderVal, 1f);
        float decibel = Mathf.Log10(valueClamp) * decibelMultiplier;
        audioMixer.SetFloat(sfxParam, decibel);
    }

    private void HandleDropdownChange(int selectValue)
    {
        if (selectValue == 0)
            UIEvents.RaiseLocaleChange(LOCALE_EN);
        else
            UIEvents.RaiseLocaleChange(LOCALE_VI);
    }
}
