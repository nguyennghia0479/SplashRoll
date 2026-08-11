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

    private readonly float duration = .5f;
    private bool isPainted;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void PaintCell()
    {
        if (isPainted)
            return;

        isPainted = true;
        StartCoroutine(ChangeColorRoutine());
        GameEvents.RaiseCellPainted();
    }

    private IEnumerator ChangeColorRoutine()
    {
        float elapseTime = 0;
        Color originalColor = spriteRenderer.color;

        while (elapseTime < duration)
        {
            Color currentColor = Color.Lerp(originalColor, paintedColor, elapseTime / duration);
            spriteRenderer.color = currentColor;

            elapseTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = paintedColor;
    }

    public CellType CellType => cellType;
}
