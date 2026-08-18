using TMPro;
using UnityEngine;
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

    private void OnEnable()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
    }

    private void OnDisable()
    {
        if (restartButton != null)
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);

        if (nextLevelButton != null)
            nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClicked);
    }

    public void SetupCompletedUI(bool canLoadNextLevel, ResultData resultData)
    {
        stageText.gameObject.SetActive(!canLoadNextLevel);
        nextLevelButton.gameObject.SetActive(canLoadNextLevel);

        levelText.text = resultData.levelName;
        movesText.text = "Moves: " + resultData.moves.ToString();
        bestText.text = "Best: " + resultData.best.ToString();
    }

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
