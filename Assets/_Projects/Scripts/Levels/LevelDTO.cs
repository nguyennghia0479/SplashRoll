
[System.Serializable]
public class LevelDTO
{
    private readonly string levelId;
    private readonly string levelName;
    private LevelData levelData;
    private bool isUnlocked;
    private bool isCompleted;
    private string gridSize;
    private int levelNumber;

    public LevelDTO(string levelId, string levelName, LevelData levelData, bool isUnlocked, bool isCompleted, string gridSize, int levelNumber)
    {
        this.levelId = levelId;
        this.levelName = levelName;
        this.levelData = levelData;
        this.isUnlocked = isUnlocked;
        this.isCompleted = isCompleted;
        this.gridSize = gridSize;
        this.levelNumber = levelNumber;
    }

    public void Unlocked() => isUnlocked = true;
    public void Complete() => isCompleted = true;

    public string LevelId => levelId;
    public string LevelName => levelName;
    public LevelData LevelData => levelData;
    public bool IsUnlocked => isUnlocked;
    public bool IsCompleted => isCompleted;
    public string GridSize => gridSize;
    public int LevelNumber => levelNumber;
}
