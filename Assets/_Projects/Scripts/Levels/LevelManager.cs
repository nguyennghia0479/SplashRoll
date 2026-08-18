using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private LevelDatabaseSO[] levelDatabases;

    private Dictionary<string, List<LevelDTO>> levelDTODict;
    private List<LevelDTO> levelDTOs;
    private LevelDTO currentLevel;
    private int currentLevelIndex;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeLevels();
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        UIEvents.OnMainMenuButtonClicked += HandleMainMenuButtonClicked;
        UIEvents.OnLevelButtonClicked += HandleLevelButtonClicked;
        UIEvents.OnRestartButtonClicked += HandleRestartButtonClicked;
        UIEvents.OnNextLevelButtonClicked += HandleNextLevelButtonClicked;
    }

    private void OnDisable()
    {
        UIEvents.OnMainMenuButtonClicked -= HandleMainMenuButtonClicked;
        UIEvents.OnLevelButtonClicked -= HandleLevelButtonClicked;
        UIEvents.OnRestartButtonClicked -= HandleRestartButtonClicked;
        UIEvents.OnNextLevelButtonClicked -= HandleNextLevelButtonClicked;
    }

    private void InitializeLevels()
    {
        levelDTODict = new Dictionary<string, List<LevelDTO>>();
        int levelIndex = 0;

        for (int i = 0; i < levelDatabases.Length; i++)
        {
            LevelSO[] levelSOs = levelDatabases[i].LevelSOs;
            List<LevelDTO> levelDTOs = new();

            for (int j = 0; j < levelSOs.Length; j++)
            {
                bool defaultUnlocked = levelIndex == 18;
                LevelSO levelSO = levelSOs[j];
                LevelDTO levelDTO = new(levelSO.LevelId, levelSO.name, levelSO.LevelData, defaultUnlocked, false);
                levelDTOs.Add(levelDTO);

                levelIndex++;
            }

            levelDTODict.Add(levelDatabases[i].LevelDBName, levelDTOs);
        }
    }

    private void HandleMainMenuButtonClicked()
    {
        levelDTOs = null;
        currentLevel = null;
        currentLevelIndex = 0;
    }

    private void HandleLevelButtonClicked(string chapterName, int currentLevelIndex)
    {
        levelDTOs = levelDTODict[chapterName];
        this.currentLevelIndex = currentLevelIndex;
        LoadLevel();
    }

    private void HandleRestartButtonClicked()
    {
        LoadLevel();
    }

    private void HandleNextLevelButtonClicked()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levelDTOs.Count)
        {
            Debug.Log("You have completed all level. Congratulations!");
            return;
        }

        LoadLevel();
    }

    private void LoadLevel()
    {
        currentLevel = levelDTOs[currentLevelIndex];
        GameEvents.RaiseLevelLoaded(currentLevel);
    }

    public bool CanLoadNextLevel()
    {
        currentLevel.Complete();
        int nextLevelIndex = currentLevelIndex + 1;
        bool canLoadNextLevel = nextLevelIndex < levelDTOs.Count;

        if (canLoadNextLevel)
            levelDTOs[nextLevelIndex].Unlocked();

        return canLoadNextLevel;
    }

    public List<LevelDTO> GetLevelsByChapter(string chapterName) => levelDTODict[chapterName];
    public int GetLevelDatabasesAmount() => levelDatabases.Length;
    public LevelDatabaseSO GetLevelDatabaseByIndex(int index) => levelDatabases[index];
    public string GetLevelName() => currentLevel.LevelName;
}
