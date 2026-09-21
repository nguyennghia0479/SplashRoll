using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompletedUI : InfoUI
{
    [Header("Ads Info")]
    [SerializeField] private InterstitialAdManager adManager;

    [Header("Text Elements")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text movesText;
    [SerializeField] private TMP_Text bestText;
    [SerializeField] private TMP_Text stageText;

    [Header("Button Elements")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextLevelButton;

    private ResultData resultData;
    private bool isMobilePlatform;

    protected override void Awake()
    {
        base.Awake();

        isMobilePlatform = false;
#if UNITY_ANDROID
        isMobilePlatform = true;
#endif
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (restartButton != null)
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);

        if (nextLevelButton != null)
            nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClicked);
    }

    public void SetupCompletedUI(bool canLoadNextLevel, ResultData resultData)
    {
        this.resultData = resultData;

        stageText.gameObject.SetActive(!canLoadNextLevel);
        nextLevelButton.gameObject.SetActive(canLoadNextLevel);

        levelLocalizedString.RefreshString();
        movesLocalizedString.RefreshString();
        bestLocalizedString.RefreshString();
    }

    protected override void UpdateLevelText(string value) => levelText.text = string.Format(value, resultData.gridSize, resultData.levelNumber);

    protected override void UpdateMovesText(string value) => movesText.text = string.Format(value, resultData.moves);

    protected override void UpdateBestText(string value) => bestText.text = string.Format(value, resultData.best);

    private void OnRestartButtonClicked()
    {
        if (adManager != null && isMobilePlatform)
            adManager.ShowAd(ProceedRestartButtonClicked);
        else
            ProceedRestartButtonClicked();
    }

    private void ProceedRestartButtonClicked()
    {
        UIEvents.RaiseRestartButtonClicked();
        gameObject.SetActive(false);
    }

    private void OnMainMenuButtonClicked()
    {
        if (adManager != null && isMobilePlatform)
            adManager.ShowAd(ProceedMainMenuButtonClicked);
        else
            ProceedMainMenuButtonClicked();
    }

    private void ProceedMainMenuButtonClicked()
    {
        UIEvents.RaiseMainMenuButtonClicked();
        gameObject.SetActive(false);
    }

    private void OnNextLevelButtonClicked()
    {
        if (adManager != null && isMobilePlatform)
            adManager.ShowAd(ProceedNextLevelButtonClicked);
        else
            ProceedNextLevelButtonClicked();
    }

    private void ProceedNextLevelButtonClicked()
    {
        UIEvents.RaiseNextLevelButtonClicked();
        gameObject.SetActive(false);
    }
}
