using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class CompletedUI : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text movesText;
    [SerializeField] private TMP_Text bestText;
    [SerializeField] private TMP_Text stageText;

    [Header("Button Elements")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextLevelButton;

    [Header("Localization Elements")]
    [SerializeField] private string tableReference;
    [Space]
    [SerializeField] private LocalizedString levelLocalizedString;
    [SerializeField] private string levelKey;

    [Space]
    [SerializeField] private LocalizedString movesLocalizedString;
    [SerializeField] private string movesKey;

    private ResultData resultData;

    private void Awake()
    {
        levelLocalizedString = new(tableReference, levelKey);
        movesLocalizedString = new(tableReference, movesKey);
    }

    private void OnEnable()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);

        levelLocalizedString.StringChanged += UpdateLevelText;
        movesLocalizedString.StringChanged += UpdateMovesText;
    }

    private void OnDisable()
    {
        if (restartButton != null)
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);

        if (nextLevelButton != null)
            nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClicked);

        levelLocalizedString.StringChanged -= UpdateLevelText;
        movesLocalizedString.StringChanged -= UpdateMovesText;
    }

    public void SetupCompletedUI(bool canLoadNextLevel, ResultData resultData)
    {
        this.resultData = resultData;

        stageText.gameObject.SetActive(!canLoadNextLevel);
        nextLevelButton.gameObject.SetActive(canLoadNextLevel);

        levelLocalizedString.RefreshString();
        movesLocalizedString.RefreshString();
        bestText.text = "Best: " + resultData.best.ToString();
    }

    private void UpdateLevelText(string value) => levelText.text = string.Format(value, resultData.gridSize, resultData.levelNumber);

    private void UpdateMovesText(string value) => movesText.text = string.Format(value, resultData.moves);

    private void OnRestartButtonClicked()
    {
        UIEvents.RaiseRestartButtonClicked();
        gameObject.SetActive(false);
    }

    private void OnMainMenuButtonClicked()
    {
        UIEvents.RaiseMainMenuButtonClicked();
        gameObject.SetActive(false);
    }

    private void OnNextLevelButtonClicked()
    {
        UIEvents.RaiseNextLevelButtonClicked();
        gameObject.SetActive(false);
    }
}
