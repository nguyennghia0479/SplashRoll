using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        tutorialButton.onClick.AddListener(OnTutorialButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        settingsButton.onClick.AddListener(OnSettingButtonClicked);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        tutorialButton.onClick.RemoveListener(OnTutorialButtonClicked);
        creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        settingsButton.onClick.RemoveListener(OnSettingButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        UIEvents.RaisePlayButtonClicked();
    }

    public void OnTutorialButtonClicked()
    {
        UIEvents.RaiseTutorialButtonClicked();
    }

    private void OnCreditsButtonClicked()
    {
        UIEvents.RaiseCreditsButtonClicked();
    }

    private void OnQuitButtonClicked()
    {
        UIEvents.RaiseButtonClicked();
        Application.Quit();
    }

    private void OnSettingButtonClicked()
    {
        UIEvents.RaiseSettingsButtonClicked();
    }
}
