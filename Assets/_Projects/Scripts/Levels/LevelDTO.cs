
[System.Serializable]
public class LevelDTO
{
    private readonly string levelId;
    private LevelData levelData;
    private bool isUnlocked;
    private bool isCompleted;
    private readonly string gridSize;
    private readonly int levelNumber;
    private int best;

    public LevelDTO(string levelId, LevelData levelData, bool isUnlocked, bool isCompleted, string gridSize, int levelNumber)
    {
        this.levelId = levelId;
        this.levelData = levelData;
        this.isUnlocked = isUnlocked;
        this.isCompleted = isCompleted;
        this.gridSize = gridSize;
        this.levelNumber = levelNumber;
    }

    public void Unlocked() => isUnlocked = true;
    public void Complete() => isCompleted = true;
    public void UpdateBest(int best) => this.best = best;

    public string LevelId => levelId;
    public LevelData LevelData => levelData;
    public bool IsUnlocked => isUnlocked;
    public bool IsCompleted => isCompleted;
    public string GridSize => gridSize;
    public int LevelNumber => levelNumber;
    public int Best => best;
}
