using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [Range(3, 8)]
    [SerializeField] private int width = 4;
    [Range (3, 10)]
    [SerializeField] private int height = 6;
    [SerializeField] private float cellSize = 1;
    [SerializeField] private float uiPadding = 1;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        SetOrthographicSizeByGridSize();
        float startX = -((width - 1) * cellSize) / 2;
        float startY = -((height - 1) * cellSize) / 2;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 spawnPos = new(startX + (i * cellSize), startY + (j * cellSize));
                Instantiate(cellPrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    private void SetOrthographicSizeByGridSize()
    {
        float targetWidth = width * cellSize;
        float targetHeight = height * cellSize;

        Camera mainCam = Camera.main;
        float cameraSizeByHeight = (targetHeight / 2) + uiPadding;
        float cameraSizeByWidth = ((targetWidth / 2) / mainCam.aspect) + 1;
        mainCam.orthographicSize = Mathf.Max(cameraSizeByHeight, cameraSizeByWidth);
    }

}
