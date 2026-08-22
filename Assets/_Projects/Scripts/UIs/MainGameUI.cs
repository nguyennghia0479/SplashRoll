using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : InfoUI
{
    [Header("Text Elements")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text movesText;
    [SerializeField] private TMP_Text bestText;

    [Header("Button Elements")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button restartButton;

    private int currentMoves;
    private LevelDTO levelDTO;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEvents.OnBallMoved += HandleBallMoved;

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingButtonClicked);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEvents.OnBallMoved -= HandleBallMoved;

        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);

        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(OnSettingButtonClicked);

        if (restartButton != null)
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    public void SetupMainGameUI(LevelDTO levelDTO)
    {
        this.levelDTO = levelDTO;
        currentMoves = 0;
        
        levelLocalizedString.RefreshString();
        movesLocalizedString.RefreshString();
        bestLocalizedString.RefreshString();
    }

    private void HandleBallMoved()
    {
        currentMoves++;
        movesLocalizedString.RefreshString();
    }

    protected override void UpdateLevelText(string value) => levelText.text = string.Format(value, levelDTO.GridSize, levelDTO.LevelNumber);

    protected override void UpdateMovesText(string value) => movesText.text = string.Format(value, currentMoves);

    protected override void UpdateBestText(string value) => bestText.text = string.Format(value, levelDTO.Best);

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
