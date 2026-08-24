using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<LevelDTO> OnLevelLoaded;
    public static event Action<int> OnUnpaintedCellCounted;
    public static event Action OnCellPainted;
    public static event Action OnBallMoved;
    public static event Action<bool, ResultData> OnLevelCompleted;

    public static void RaiseLevelLoaded(LevelDTO levelDTO)
    {
        OnLevelLoaded?.Invoke(levelDTO);
    }

    public static void RaiseUnpaintedCellCounted(int unpaintedCellAmount)
    {
        OnUnpaintedCellCounted?.Invoke(unpaintedCellAmount);
    }

    public static void RaiseCellPainted()
    {
        OnCellPainted?.Invoke();
    }

    public static void RaiseBallMoved()
    {
        OnBallMoved?.Invoke();
    }

    public static void RaiseLevelCompleted(bool canLoadNextLevel, ResultData resultData)
    {
        OnLevelCompleted?.Invoke(canLoadNextLevel, resultData);
    }
}
