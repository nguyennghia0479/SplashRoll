
[System.Serializable]
public class LevelDTO
{
    private readonly string levelId;
    private readonly string levelName;
    private LevelData levelData;
    private bool isUnlocked;
    private bool isCompleted;

    public LevelDTO(string levelId, string levelName, LevelData levelData, bool isUnlocked, bool isCompleted)
    {
        this.levelId = levelId;
        this.levelName = levelName;
        this.levelData = levelData;
        this.isUnlocked = isUnlocked;
        this.isCompleted = isCompleted;
    }

    public void Unlocked() => isUnlocked = true;
    public void Complete() => isCompleted = true;

    public string LevelId => levelId;
    public string LevelName => levelName;
    public LevelData LevelData => levelData;
    public bool IsUnlocked => isUnlocked;
    public bool IsCompleted => isCompleted;
}
