using TMPro;
using UnityEngine;
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

    private int moves;

    private void OnEnable()
    {
        GameEvents.OnBallMoved += HandleBallMoved;

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingButtonClicked);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonClicked);
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
    }

    public void SetupMainGameUI(LevelDTO levelDTO)
    {
        moves = 0;

        levelText.text = levelDTO.LevelName;
        movesText.text = "Moves: " + moves;
        bestText.text = "Best: ";
    }

    private void HandleBallMoved()
    {
        moves++;
        movesText.text = "Moves: " + moves.ToString();
    }

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
