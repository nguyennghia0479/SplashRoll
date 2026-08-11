using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Cell wallPrefab;

    [Header("Ball Info")]
    [SerializeField] private BallMovement ballPrefab;
    [SerializeField] private float ballSize = .8f;

    private int width;
    private int height;
    private float cellSize;
    private float uiPadding;
    private List<Vector2Int> wallCoordinates;
    private Vector2Int ballSpawnCoordinate;
    private List<Cell> pathCells;
    private Cell[,] gridData;
    private float gridStartX;
    private float gridStartY;
    private int emptyCellAmount;
    private GameObject ball;

    private void OnEnable()
    {
        GameEvents.OnLevelLoaded += HandleLoadLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelLoaded -= HandleLoadLevel;
    }

    private void HandleLoadLevel(LevelSO level)
    {
        width = level.GridWidth;
        height = level.GridHeight;
        cellSize = level.CellSize;
        uiPadding = level.UIPadding;
        wallCoordinates = level.WallCoordinates;
        ballSpawnCoordinate = level.BallSpawnCoordinate;

        SetupGrid();
        SetOrthographicSizeByGridSize();
        GenerateGrid();
        GenerateBall();
    }

    private void SetupGrid()
    {
        pathCells = new List<Cell>();
        gridData = new Cell[width, height];
        emptyCellAmount = 0;
        Destroy(ball);
    }

    private void GenerateGrid()
    {
        gridStartX = -((width - 1) * cellSize) / 2;
        gridStartY = -((height - 1) * cellSize) / 2;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 spawnPos = new(gridStartX + (i * cellSize), gridStartY + (j * cellSize));
                Vector2Int currentCoord = new(i, j);

                if (wallCoordinates.Contains(currentCoord))
                {
                    Cell newWall = Instantiate(wallPrefab, spawnPos, Quaternion.identity, transform);
                    gridData[i, j] = newWall;
                }
                else
                {
                    Cell newCell = Instantiate(cellPrefab, spawnPos, Quaternion.identity, transform);
                    gridData[i, j] = newCell;
                    emptyCellAmount++;
                }
            }
        }

        GameEvents.RaiseEmptyCellCounted(emptyCellAmount);
    }

    private void GenerateBall()
    {
        if (!IsValidCoordinate(ballSpawnCoordinate.x, ballSpawnCoordinate.y))
        {
            Debug.Log("Ball is outside grid");
            return;
        }

        gridData[ballSpawnCoordinate.x, ballSpawnCoordinate.y].PaintCell();
        Vector2 spawnPos = GridCoordinateToWorldPostion(ballSpawnCoordinate.x, ballSpawnCoordinate.y);
        BallMovement newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        newBall.transform.localScale = new Vector3(cellSize * ballSize, cellSize * ballSize, 1);
        newBall.SetupBallMovement(this);
        ball = newBall.gameObject;
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

    public Vector3 ClampToGridBoundaries(Vector3 targetPosition)
    {
        float halfWidth = (width * cellSize) / 2;
        float halfHeight = (height * cellSize) / 2;

        float minX = -halfWidth + (cellSize / 2);
        float maxX = halfWidth - (cellSize / 2);
        float minY = -halfHeight + (cellSize / 2);
        float maxY = halfHeight - (cellSize / 2);

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        return targetPosition;
    }

    private Vector2 GridCoordinateToWorldPostion(int coordX, int coordY)
    {
        float worldX = gridStartX + (coordX * cellSize);
        float worldY = gridStartY + (coordY * cellSize);

        return new Vector2(worldX, worldY);
    }

    private Vector2Int WorldPositonToGridCoordinate(Vector3 worldPositon)
    {
        int coordX = Mathf.RoundToInt((worldPositon.x - gridStartX) / cellSize);
        int coordY = Mathf.RoundToInt((worldPositon.y - gridStartY) / cellSize);

        return new Vector2Int(coordX, coordY);
    }

    private bool IsValidCoordinate(int coordX, int coordY)
    {
        return coordX >= 0 && coordX < width && coordY >= 0 && coordY < height;
    }

    public bool IsWallCell(Vector3 worldPosition)
    {
        Vector2Int coordinate = WorldPositonToGridCoordinate(worldPosition);
        if (!IsValidCoordinate(coordinate.x, coordinate.y))
            return true;

        Cell cell = gridData[coordinate.x, coordinate.y];
        if (cell.CellType == CellType.Empty)
            pathCells.Add(cell);

        return cell.CellType == CellType.Wall;
    }

    public void PaintCell(Vector3 ballPosition)
    {
        if (pathCells.Count <= 0)
            return;

        Cell cell = pathCells[0];
        if ((cell.transform.position - ballPosition).sqrMagnitude < .1f)
        {
            cell.PaintCell();
            pathCells.RemoveAt(0);
        }
    }

    public void ClearPathCells() => pathCells.Clear();
}
