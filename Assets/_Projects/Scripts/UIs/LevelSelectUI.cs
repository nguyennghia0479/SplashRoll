using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private TMP_Text levelDBText;
    [Header("Button Elements")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    private LevelManager levelManager;
    private LevelDatabaseSO currentLevelDB;
    private int currentLevelDBIndex;
    private int levelDBAmount;
    private LevelUI[] levelUIs;

    private void Awake()
    {
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

        UpdateLevelSelectUI();
    }

    private void OnDisable()
    {
        if (previousButton != null)
            previousButton.onClick.RemoveListener(OnPreviousButtonClicked);

        if (nextButton != null)
            nextButton.onClick.RemoveListener(OnNextButtonClicked);
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
        levelDBText.text = currentLevelDB.LevelDBName;
    }

    private void UpdateLevelUIs()
    {
        List<LevelDTO> levelDTOs = levelManager.GetLevelsByChapter(currentLevelDB.LevelDBName);

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
