using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;
    [Space]
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private LevelSelectUI levelSelectUI;
    [SerializeField] private CreditsUI creditsUI;
    [SerializeField] private SettingsUI settingsUI;

    private void OnEnable()
    {
        UIEvents.OnPlayButtonClicked += HandlePlayButtonClicked;
        UIEvents.OnCreditsButtonClicked += HandleCreditButtonClicked;
        UIEvents.OnSettingsButtonClicked += HandleSettingsButtonClicked;
    }

    private void OnDisable()
    {
        UIEvents.OnPlayButtonClicked -= HandlePlayButtonClicked;
        UIEvents.OnCreditsButtonClicked -= HandleCreditButtonClicked;
        UIEvents.OnSettingsButtonClicked -= HandleSettingsButtonClicked;
    }

    private void Start()
    {
        SwitchToUI(mainMenuUI.gameObject);
    }

    private void SwitchToUI(GameObject uiToEnable)
    {
        foreach (var uiElement in uiElements)
            uiElement.SetActive(false);

        uiToEnable.SetActive(true);
    }

    private void HandlePlayButtonClicked()
    {
        levelSelectUI.gameObject.SetActive(true);
    }

    private void HandleCreditButtonClicked()
    {
        creditsUI.gameObject.SetActive(true);
    }

    private void HandleSettingsButtonClicked()
    {
        settingsUI.gameObject.SetActive(true);
    }

    public void OnCloseButtonClicked(GameObject uiElement)
    {
        uiElement.SetActive(false);
    }
}
