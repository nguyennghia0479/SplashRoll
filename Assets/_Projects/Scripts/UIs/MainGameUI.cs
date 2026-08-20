using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text movesText;
    [SerializeField] private TMP_Text bestText;

    [Header("Button Elements")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button restartButton;

    [Header("Localization Elements")]
    [SerializeField] private string tableReference;
    [Space]
    [SerializeField] private LocalizedString levelLocalizedString;
    [SerializeField] private string levelKey;

    [Space]
    [SerializeField] private LocalizedString movesLocalizedString;
    [SerializeField] private string movesKey;

    private int currentMoves;
    private LevelDTO levelDTO;

    private void Awake()
    {
        levelLocalizedString = new(tableReference, levelKey);
        movesLocalizedString = new(tableReference, movesKey);
    }

    private void OnEnable()
    {
        GameEvents.OnBallMoved += HandleBallMoved;

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingButtonClicked);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonClicked);

        levelLocalizedString.StringChanged += UpdateLevelText;
        movesLocalizedString.StringChanged += UpdateMovesText;
    }

    private void OnDisable()
    {
        GameEvents.OnBallMoved -= HandleBallMoved;

        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);

        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(OnSettingButtonClicked);

        if (restartButton != null)
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);

        levelLocalizedString.StringChanged -= UpdateLevelText;
        movesLocalizedString.StringChanged -= UpdateMovesText;
    }

    public void SetupMainGameUI(LevelDTO levelDTO)
    {
        this.levelDTO = levelDTO;
        currentMoves = 0;
        
        bestText.text = "Best: ";

        levelLocalizedString.RefreshString();
        movesLocalizedString.RefreshString();
    }

    private void HandleBallMoved()
    {
        currentMoves++;
        movesLocalizedString.RefreshString();
    }

    private void UpdateLevelText(string value) => levelText.text = string.Format(value, levelDTO.GridSize, levelDTO.LevelNumber);

    private void UpdateMovesText(string value) => movesText.text = string.Format(value, currentMoves);

    private void OnMainMenuButtonClicked()
    {
        UIEvents.RaiseMainMenuButtonClicked();
    }

    private void OnSettingButtonClicked()
    {
        UIEvents.RaiseSettingsButtonClicked();
    }

    private void OnRestartButtonClicked()
    {
        UIEvents.RaiseRestartButtonClicked();
    }
}
