using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<LevelSO> OnLevelLoaded;
    public static event Action<int> OnEmptyCellCounted;
    public static event Action OnCellPainted;

    public static void RaiseLevelLoaded(LevelSO level)
    {
        OnLevelLoaded?.Invoke(level);
    }

    public static void RaiseEmptyCellCounted(int emptyCellAmount)
    {
        OnEmptyCellCounted?.Invoke(emptyCellAmount);
    }

    public static void RaiseCellPainted()
    {
        OnCellPainted?.Invoke();
    }
}
