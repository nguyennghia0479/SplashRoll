#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    [Header("Grid Configuration")]
    [Range(3, 8)]
    [SerializeField] private int width = 3;
    [Range(3, 10)]
    [SerializeField] private int height = 3;
    [Range(.5f, 1.5f)]
    [SerializeField] private float cellSize = 1;
    [Range(1, 5)]
    [SerializeField] private float uiPadding = 3;

    [Header("References")]
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private LevelSO levelSO;

    public void GenerateGrid()
    {
        ClearGrid();

        if (gridCellPrefab == null )
        {
            Debug.LogError("[GridBuilder] not have assigned GridCell Prefab");
            return;
        }

        SetOrthographicSizeByGridSize();
        float gridStartX = -((width - 1) * cellSize) / 2f;
        float gridStartY = -((height - 1) * cellSize) / 2f;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 spawnPos = new(gridStartX + (i * cellSize), gridStartY + (j * cellSize));
                GameObject newGridCell = PrefabUtility.InstantiatePrefab(gridCellPrefab, transform) as GameObject;
                newGridCell.transform.localPosition = spawnPos;
                Undo.RegisterCreatedObjectUndo(newGridCell, "Generate Grid");

                newGridCell.name = $"Cell_{i}_{j}";
                if (newGridCell.TryGetComponent<CellBuilder>(out var gridCell))
                {
                    gridCell.SetType(CellType.Empty);
                    gridCell.SetCoordinate(i, j);
                }
            }
        }
    }

    private void SetOrthographicSizeByGridSize()
    {
        Camera mainCam = Camera.main;
        float maxWidth = width * cellSize;
        float maxHeight = height * cellSize;
        float cameraSizeByWidth = ((maxWidth / 2) / mainCam.aspect) + 1;
        float cameraSizeByHeight = (maxHeight / 2) + uiPadding;

        mainCam.orthographicSize = Mathf.Max(cameraSizeByWidth, cameraSizeByHeight);
    }

    public void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Undo.DestroyObjectImmediate(transform.GetChild(i).gameObject);
    }

    public void SaveToLevelSO()
    {
        if (levelSO == null)
        {
            Debug.LogError("[GridBuilder] not have assigned LevelSO");
            return;
        }

        CellBuilder[] gridData = GetComponentsInChildren<CellBuilder>();
        if (gridData.Length <= 0)
        {
            Debug.Log("Nothing to save");
            return;
        }

        Undo.RecordObject(levelSO, "Save LevelSO Data");
        List<Vector2Int> wallCoordinates = new();
        Vector2Int ballStartCoord = Vector2Int.zero;

        foreach (CellBuilder cell in gridData)
        {
            Vector2Int coord = new(cell.CoordX, cell.CoordY);
            
            if (cell.CellType == CellType.Wall)
                wallCoordinates.Add(coord);
            else if (cell.CellType == CellType.Start)
                ballStartCoord = coord;
        }

        LevelData levelData = new(width, height, cellSize, uiPadding, wallCoordinates, ballStartCoord);
        levelSO.SaveLevelSO(levelData);
        EditorUtility.SetDirty(levelSO);
        AssetDatabase.SaveAssets();
        Debug.Log("Save Successfully");
    }

    public void LoadFromLevelSO()
    {
        if (levelSO == null)
        {
            Debug.LogError("[GridBuilder] not have assigned LevelSO");
            return;
        }

        width = levelSO.GridWidth;
        height = levelSO.GridHeight;
        cellSize = levelSO.CellSize;
        uiPadding = levelSO.UIPadding;
        GenerateGrid();

        CellBuilder[] gridData = GetComponentsInChildren<CellBuilder>();
        foreach (CellBuilder cell in gridData)
        {
            Vector2Int coord = new(cell.CoordX, cell.CoordY);

            if (levelSO.WallCoordinates.Contains(coord))
                cell.SetType(CellType.Wall);
            else if (levelSO.BallStartCoord == coord)
                cell.SetType(CellType.Start);
        }
        Debug.Log("Load Successfully");
    }
}
#endif