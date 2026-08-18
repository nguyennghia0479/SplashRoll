using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;
    [Space]
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private MainGameUI mainGameUI;
    [SerializeField] private CompletedUI completedUI;
    [SerializeField] private LevelSelectUI levelSelectUI;
    [SerializeField] private CreditsUI creditsUI;
    [SerializeField] private SettingsUI settingsUI;

    private void OnEnable()
    {
        UIEvents.OnPlayButtonClicked += HandlePlayButtonClicked;
        UIEvents.OnCreditsButtonClicked += HandleCreditButtonClicked;
        UIEvents.OnSettingsButtonClicked += HandleSettingsButtonClicked;
        UIEvents.OnLevelButtonClicked += HandleLevelButtonClicked;
        UIEvents.OnMainMenuButtonClicked += HandleMainMenuButtonClicked;
        GameEvents.OnLevelLoaded += HandleLevelLoaded;
        GameEvents.OnLevelCompleted += HandleLevelCompleted;
    }

    private void OnDisable()
    {
        UIEvents.OnPlayButtonClicked -= HandlePlayButtonClicked;
        UIEvents.OnCreditsButtonClicked -= HandleCreditButtonClicked;
        UIEvents.OnSettingsButtonClicked -= HandleSettingsButtonClicked;
        UIEvents.OnLevelButtonClicked -= HandleLevelButtonClicked;
        UIEvents.OnMainMenuButtonClicked -= HandleMainMenuButtonClicked;
        GameEvents.OnLevelLoaded -= HandleLevelLoaded;
        GameEvents.OnLevelCompleted -= HandleLevelCompleted;
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

    private void HandleLevelButtonClicked(string chapterName, int currentLevelIndex)
    {
        SwitchToUI(mainGameUI.gameObject);
    }

    private void HandleLevelLoaded(LevelDTO levelDTO)
    {
        mainGameUI.SetupMainGameUI(levelDTO);
    }

    private void HandleLevelCompleted(bool canLoadNextLevel, ResultData resultData)
    {
        completedUI.SetupCompletedUI(canLoadNextLevel, resultData);
        completedUI.gameObject.SetActive(true);
    }

    private void HandleMainMenuButtonClicked()
    {
        SwitchToUI(mainMenuUI.gameObject);
    }

    public void OnCloseButtonClicked(GameObject uiElement)
    {
        uiElement.SetActive(false);
    }
}
