using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level -", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private bool isCompleted;

    public void SaveLevelSO(LevelData levelData)
    {
        this.levelData = levelData;
    }

    public LevelData LevelData => levelData;
    public bool IsUnlocked => isUnlocked;
    public bool IsCompleted => isCompleted;
    public int GridWidth => levelData.gridWidth;
    public int GridHeight => levelData.gridHeight;
    public float CellSize => levelData.cellSize;
    public float UIPadding => levelData.uiPadding;
    public IReadOnlyList<Vector2Int> WallCoordinates => levelData.wallCoordinates;
    public Vector2Int BallStartCoord => levelData.ballStartCoord;
}

[System.Serializable]
public struct LevelData
{
    public int gridWidth;
    public int gridHeight;
    public float cellSize;
    public float uiPadding;
    public List<Vector2Int> wallCoordinates;
    public Vector2Int ballStartCoord;

    public LevelData(int gridWidth, int gridHeight, float cellSize, float uiPadding, List<Vector2Int> wallCoordinates, Vector2Int ballStartCoord)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.cellSize = cellSize;
        this.uiPadding = uiPadding;
        this.wallCoordinates = wallCoordinates != null ? new List<Vector2Int>(wallCoordinates) : new();
        this.ballStartCoord = ballStartCoord;
    }
}
