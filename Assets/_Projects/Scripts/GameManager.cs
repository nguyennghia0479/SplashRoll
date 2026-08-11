using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int emptyCellAmount;

    private void OnEnable()
    {
        GameEvents.OnEmptyCellCounted += HandleEmptyCellCounted;
        GameEvents.OnCellPainted += HandleCellPainted;
    }

    private void OnDisable()
    {
        GameEvents.OnEmptyCellCounted -= HandleEmptyCellCounted;
        GameEvents.OnCellPainted -= HandleCellPainted;
    }

    private void HandleEmptyCellCounted(int emptyCellAmount)
    {
        this.emptyCellAmount = emptyCellAmount;
    }

    private void HandleCellPainted()
    {
        if (emptyCellAmount <= 0)
            return;

        emptyCellAmount--;
        if (emptyCellAmount <= 0)
            Debug.Log("Complete level");
    }
}
