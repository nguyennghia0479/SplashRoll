using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [Header("Localization Elements")]
    [SerializeField] private LocalizedString stageLocalizedString;
    [SerializeField] private string tableReference;
    [SerializeField] private string stageKey;
    [SerializeField] private TMP_Text stageText;

    [Header("Button Elements")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    private LevelManager levelManager;
    private LevelDatabaseSO currentLevelDB;
    private int currentLevelDBIndex;
    private int levelDBAmount;
    private LevelUI[] levelUIs;
    private string stageName;

    private void Awake()
    {
        stageLocalizedString = new(tableReference, stageKey);
        levelUIs = GetComponentsInChildren<LevelUI>();
    }

    private void OnEnable()
    {
        if (levelManager == null)
            return;

        if (previousButton != null)
            previousButton.onClick.AddListener(OnPreviousButtonClicked);

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextButtonClicked);

        stageLocalizedString.StringChanged += UpdateStageText;
        UpdateLevelSelectUI();
    }

    private void OnDisable()
    {
        if (previousButton != null)
            previousButton.onClick.RemoveListener(OnPreviousButtonClicked);

        if (nextButton != null)
            nextButton.onClick.RemoveListener(OnNextButtonClicked);

        stageLocalizedString.StringChanged -= UpdateStageText;
    }

    private void Start()
    {
        levelManager = LevelManager.Instance;
        levelDBAmount = levelManager.GetLevelDatabasesAmount();
    }

    private void UpdateLevelSelectUI()
    {
        currentLevelDB = levelManager.GetLevelDatabaseByIndex(currentLevelDBIndex);
        UpdateLevelDBName();
        UpdateLevelUIs();
    }

    private void OnPreviousButtonClicked()
    {
        currentLevelDBIndex--;
        if (currentLevelDBIndex < 0)
            currentLevelDBIndex = levelDBAmount - 1;

        UpdateLevelSelectUI();
    }

    private void OnNextButtonClicked()
    {
        currentLevelDBIndex++;
        if (currentLevelDBIndex > levelDBAmount - 1)
            currentLevelDBIndex = 0;

        UpdateLevelSelectUI();
    }

    private void UpdateLevelDBName()
    {
        stageName = currentLevelDB.LevelDBName;
        stageLocalizedString.RefreshString();
    }

    private void UpdateStageText(string value) => stageText.text = string.Format(value, stageName);

    private void UpdateLevelUIs()
    {
        List<LevelDTO> levelDTOs = levelManager.GetLevelsByChapter(currentLevelDB.LevelDBName);
        levelManager.UpdateCurrentLevelDatabaseIndex(currentLevelDBIndex);

        foreach (var levelUI in levelUIs)
            levelUI.gameObject.SetActive(false);

        for (int i = 0; i < levelUIs.Length; i++)
        {
            LevelUI levelUI = levelUIs[i];

            if (i < levelDTOs.Count)
            {
                levelUI.SetupLevelUI(i, currentLevelDB.LevelDBName, levelDTOs[i]);
                levelUI.gameObject.SetActive(true);
            }
            else
                levelUI.gameObject.SetActive(false);
        }
    }
}
