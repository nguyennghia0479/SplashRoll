using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level -", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [Header("Grid Info")]
    [Range(3, 8)]
    [SerializeField] private int gridWitdth;
    [Range(3, 10)]
    [SerializeField] private int gridHeight;
    [Range(.5f, 1.5f)]
    [SerializeField] private float cellSize = 1;
    [SerializeField] private float uiPadding = 1;
    [SerializeField] private List<Vector2Int> wallCoordinates;
    [SerializeField] private Vector2Int ballSpawnCoordinate;

    public int GridWidth => gridWitdth;
    public int GridHeight => gridHeight;
    public float CellSize => cellSize;
    public float UIPadding => uiPadding;
    public List<Vector2Int> WallCoordinates => wallCoordinates;
    public Vector2Int BallSpawnCoordinate => ballSpawnCoordinate;
}
