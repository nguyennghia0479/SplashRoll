using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private Image completeIcon;
    [SerializeField] private TMP_Text levelText;

    private int level;
    private LevelSO levelSO;

    private void OnEnable()
    {
        levelButton.onClick.AddListener(OnLevelButtonClicked);
    }

    private void OnDisable()
    {
        levelButton.onClick.RemoveListener(OnLevelButtonClicked);
    }

    public void SetupLevelUI(int level, LevelSO levelSO)
    {
        this.level = level;
        this.levelSO = levelSO;
        UpdateLevelButton();
    }

    private void UpdateLevelButton()
    {
        levelButton.interactable = levelSO.IsUnlocked;
        levelText.text = levelSO.IsUnlocked ? level.ToString() : "X";
        completeIcon.gameObject.SetActive(levelSO.IsCompleted);
    }

    private void OnLevelButtonClicked()
    {
        Debug.Log("Load level " + levelSO.name);
    }
}
