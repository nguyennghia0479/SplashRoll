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
    private int currentLevelDBIndex;

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
                bool defaultUnlocked = levelIndex == 0;
                LevelSO levelSO = levelSOs[j];
                LevelDTO levelDTO = new(levelSO.LevelId, levelSO.LevelData, defaultUnlocked, false, levelSO.GridSize, levelSO.LevelNumber);
                
                LevelSaveData dataLoaded = SaveManager.LoadLevel(levelSO.LevelId);
                if (dataLoaded != null)
                {
                    if (dataLoaded.isUnlocked)
                        levelDTO.Unlocked();

                    if (dataLoaded.isCompleted)
                        levelDTO.Complete();

                    levelDTO.UpdateBest(dataLoaded.best);
                }

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

    private void HandleLevelButtonClicked(string stageName, int currentLevelIndex)
    {
        levelDTOs = levelDTODict[stageName];
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
        int nextLevelIndex = currentLevelIndex + 1;
        bool canLoadNextLevel = nextLevelIndex < levelDTOs.Count;

        if (canLoadNextLevel)
            UnlockNextLevel(nextLevelIndex);
        else
            UnlockNextStage();

        return canLoadNextLevel;
    }

    public void CompleteLevel(int moves)
    {
        currentLevel.Complete();
        SaveLevelCompleted(moves);
    }

    private void UnlockNextLevel(int nextLevelIndex)
    {
        LevelDTO nextLevel = levelDTOs[nextLevelIndex];
        nextLevel.Unlocked();
        SaveNextLevelUnlocked(nextLevel);
    }

    private void UnlockNextStage()
    {
        currentLevelDBIndex++;
        if (currentLevelDBIndex >= levelDTOs.Count)
        {
            Debug.Log("You have completed all level. Congratulations!");
            return;
        }

        LevelDatabaseSO levelDatabase = levelDatabases[currentLevelDBIndex];
        levelDTOs = levelDTODict[levelDatabase.LevelDBName];
        levelDTOs[0].Unlocked();
        SaveNextLevelUnlocked(levelDTOs[0]);
    }

    private void SaveLevelCompleted(int moves)
    {
        LevelSaveData dataLoaded = SaveManager.LoadLevel(currentLevel.LevelId);
        if (dataLoaded == null)
            dataLoaded = new(currentLevel.LevelId, currentLevel.IsUnlocked, currentLevel.IsCompleted, moves);
        else
        {
            dataLoaded.best = (dataLoaded.best <= 0 || dataLoaded.best > moves) ? moves : dataLoaded.best;
            dataLoaded.isCompleted = currentLevel.IsCompleted;
        }

        currentLevel.UpdateBest(dataLoaded.best);
        SaveManager.SaveLevel(currentLevel.LevelId, dataLoaded);
    }

    private void SaveNextLevelUnlocked(LevelDTO nextLevel)
    {
        LevelSaveData dataLoaded = SaveManager.LoadLevel(nextLevel.LevelId);
        if (dataLoaded == null)
            dataLoaded = new(nextLevel.LevelId, nextLevel.IsUnlocked, nextLevel.IsCompleted, 0);
        else
            dataLoaded.isUnlocked = nextLevel.IsUnlocked;

        SaveManager.SaveLevel(nextLevel.LevelId, dataLoaded);
    }

    public void UpdateCurrentLevelDatabaseIndex(int index) => currentLevelDBIndex = index;

    public List<LevelDTO> GetLevelsByChapter(string chapterName) => levelDTODict[chapterName];
    public int GetLevelDatabasesAmount() => levelDatabases.Length;
    public LevelDatabaseSO GetLevelDatabaseByIndex(int index) => levelDatabases[index];
    public LevelDTO GetCurrentLevel() => currentLevel;
}

[System.Serializable]
public class LevelSaveData
{
    public string levelId;
    public bool isUnlocked;
    public bool isCompleted;
    public int best;

    public LevelSaveData(string levelId, bool isUnlocked, bool isCompleted, int best)
    {
        this.levelId = levelId;
        this.isUnlocked = isUnlocked;
        this.isCompleted = isCompleted;
        this.best = best;
    }
}
