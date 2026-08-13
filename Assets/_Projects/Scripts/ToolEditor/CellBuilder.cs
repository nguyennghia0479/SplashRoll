using UnityEngine;

public class CellBuilder : MonoBehaviour
{
    [SerializeField] private Color emptyCellColor;
    [SerializeField] private Color wallCellColor;
    [SerializeField] private Color startCellColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int coordX;
    private int coordY;
    private CellType cellType = CellType.Empty;

    public void SetType(CellType cellType)
    {
#if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(this, "Change Cell Type");
#endif
        this.cellType = cellType;
        UpdateVisual();
    }

    public void SetCoordinate(int coordX, int coordY)
    {
        this.coordX = coordX;
        this.coordY = coordY;
    }

    private void UpdateVisual()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("[GridCell] have not assigned SpriteRenderer");
            return;
        }

        if (cellType == CellType.Wall)
            spriteRenderer.color = wallCellColor;
        else if (cellType == CellType.Start)
            spriteRenderer.color = startCellColor;
        else
            spriteRenderer.color = emptyCellColor;
    }

    private void OnValidate()
    {
        UpdateVisual();
    }

    public int CoordX => coordX;
    public int CoordY => coordY;
    public CellType CellType => cellType;
}
