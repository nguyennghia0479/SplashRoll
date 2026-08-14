using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private LevelDatabaseSO[] leveDatabases;

    [SerializeField] private TMP_Text levelDBText;
    [Header("Button Elements")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    private LevelDatabaseSO currentLevelDB;
    private int currentLevelDBIndex;
    private LevelUI[] levelUIs;

    private void Awake()
    {
        levelUIs = GetComponentsInChildren<LevelUI>();
    }

    private void OnEnable()
    {
        previousButton.onClick.AddListener(OnPreviousButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);

        UpdateLevelSelectUI();
    }

    private void OnDisable()
    {
        previousButton.onClick.RemoveListener(OnPreviousButtonClicked);
        nextButton.onClick.RemoveListener (OnNextButtonClicked);
    }

    private void UpdateLevelSelectUI()
    {
        currentLevelDB = leveDatabases[currentLevelDBIndex];
        UpdateLevelDBName();
        UpdateLevelUIs();
    }

    private void OnPreviousButtonClicked()
    {
        currentLevelDBIndex--;
        if (currentLevelDBIndex < 0)
            currentLevelDBIndex = leveDatabases.Length - 1;

        UpdateLevelSelectUI();
    }

    private void OnNextButtonClicked()
    {
        currentLevelDBIndex++;
        if (currentLevelDBIndex > leveDatabases.Length - 1)
            currentLevelDBIndex = 0;

        UpdateLevelSelectUI();
    }

    private void UpdateLevelDBName()
    {
        levelDBText.text = currentLevelDB.LevelDBName;
    }

    private void UpdateLevelUIs()
    {
        foreach(var levelUI in levelUIs)
            levelUI.gameObject.SetActive(false);

        int level = 1;
        for (int i = 0; i < levelUIs.Length; i++)
        {
            LevelUI levelUI = levelUIs[i];

            if (i < currentLevelDB.LevelSOs.Length)
            {
                LevelSO levelSO = currentLevelDB.LevelSOs[i];
                levelUI.SetupLevelUI(level, levelSO);
                levelUI.gameObject.SetActive(true);
                level++;
            }
            else
                levelUI.gameObject.SetActive(false);
        }
    }
}
