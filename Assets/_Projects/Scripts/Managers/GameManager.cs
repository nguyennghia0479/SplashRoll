using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelManager levelManager;
    private int unpaintedCellAmount;
    private int moves;

    private void OnEnable()
    {
        GameEvents.OnBallMoved += HandleBallMoved;

        UIEvents.OnMainMenuButtonClicked += HandleMainMenuButtonClicked;
        GameEvents.OnUnpaintedCellCounted += HandleUnpaintedCellCounted;
        GameEvents.OnCellPainted += HandleCellPainted;
    }

    private void OnDisable()
    {
        GameEvents.OnBallMoved -= HandleBallMoved;

        UIEvents.OnMainMenuButtonClicked -= HandleMainMenuButtonClicked;
        GameEvents.OnUnpaintedCellCounted -= HandleUnpaintedCellCounted;
        GameEvents.OnCellPainted -= HandleCellPainted;
    }

    private void Start()
    {
        levelManager = LevelManager.Instance;
    }

    private void HandleMainMenuButtonClicked()
    {
        unpaintedCellAmount = 0;
        moves = 0;
    }

    private void HandleUnpaintedCellCounted(int unpaintedCellAmount)
    {
        this.unpaintedCellAmount = unpaintedCellAmount;
        moves = 0;
    }

    private void HandleCellPainted()
    {
        if (unpaintedCellAmount <= 0)
            return;

        unpaintedCellAmount--;
        if (unpaintedCellAmount <= 0)
            Invoke(nameof(LevelCompleted), 1f);
    }

    private void LevelCompleted()
    {
        levelManager.CompleteLevel(moves);
        bool canLoadNextLevel = levelManager.CanLoadNextLevel();
        LevelDTO currentLevel = levelManager.GetCurrentLevel();
        ResultData resultData = new(currentLevel.GridSize, currentLevel.LevelNumber, moves, currentLevel.Best);
        GameEvents.RaiseLevelCompleted(canLoadNextLevel, resultData);
    }

    private void HandleBallMoved() => moves++;
}

public struct ResultData
{
    public string gridSize;
    public int levelNumber;
    public int moves;
    public int best;

    public ResultData(string gridSize, int levelNumber, int moves, int best)
    {
        this.gridSize = gridSize;
        this.levelNumber = levelNumber;
        this.moves = moves;
        this.best = best;
    }
}
