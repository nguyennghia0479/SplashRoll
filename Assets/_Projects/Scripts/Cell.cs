using System.Collections;
using UnityEngine;

public enum CellType
{
    Empty, Wall
}

public class Cell : MonoBehaviour
{
    [SerializeField] private CellType cellType;
    [SerializeField] private Color paintedColor;
    [SerializeField] private SpriteRenderer spriteCell;

    private readonly float duration = .5f;
    private bool isPainted;

    public void PaintCell()
    {
        if (isPainted || cellType == CellType.Wall)
            return;

        isPainted = true;
        StartCoroutine(ChangeColorRoutine());
        GameEvents.RaiseCellPainted();
    }

    private IEnumerator ChangeColorRoutine()
    {
        float elapseTime = 0;
        Color originalColor = spriteCell.color;

        while (elapseTime < duration)
        {
            Color currentColor = Color.Lerp(originalColor, paintedColor, elapseTime / duration);
            spriteCell.color = currentColor;

            elapseTime += Time.deltaTime;
            yield return null;
        }

        spriteCell.color = paintedColor;
    }

    public CellType CellType => cellType;
}
