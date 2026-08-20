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

    private readonly float minSliderVal = .0001f;

    private void OnEnable()
    {
        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(HandleSFXSlider);
    }

    private void OnDisable()
    {
        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(HandleSFXSlider);
    }

    private void HandleSFXSlider(float sliderValue)
    {
        float valueClamp = Mathf.Clamp(sliderValue, minSliderVal, 1f);
        float decibel = Mathf.Log10(valueClamp) * decibelMultiplier;
        audioMixer.SetFloat(sfxParam, decibel);
    }
}
