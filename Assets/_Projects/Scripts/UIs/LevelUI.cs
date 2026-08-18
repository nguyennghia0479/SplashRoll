using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private Image completeIcon;
    [SerializeField] private TMP_Text levelText;

    private int level;
    private string levelDBName;
    private LevelDTO levelDTO;

    private void OnEnable()
    {
        levelButton.onClick.AddListener(OnLevelButtonClicked);
    }

    private void OnDisable()
    {
        levelButton.onClick.RemoveListener(OnLevelButtonClicked);
    }

    public void SetupLevelUI(int level, string levelDBName, LevelDTO levelDTO)
    {
        this.level = level;
        this.levelDBName = levelDBName;
        this.levelDTO = levelDTO;

        UpdateLevelButton();
    }

    private void UpdateLevelButton()
    {
        levelButton.interactable = levelDTO.IsUnlocked;
        levelText.text = levelDTO.IsUnlocked ? (level + 1).ToString() : "X";
        completeIcon.gameObject.SetActive(levelDTO.IsCompleted);
    }

    private void OnLevelButtonClicked()
    {
        UIEvents.RaiseLevelButtonClicked(levelDBName, level);
    }
}
