using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<LevelSO> levels;

    private LevelSO currentLevel;
    private int levelIndex;
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

    private void Start()
    {
        levelIndex = 0;
        LoadLevel();
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
            Invoke(nameof(LoadNextLevel), 1f);  // Will be replaced by UI
    }

    private void LoadNextLevel()
    {
        levelIndex++;
        if (levelIndex >= levels.Count)
        {
            Debug.Log("You have completed all level. Congratulations!");
            return;
        }

        LoadLevel();
    }

    private void LoadLevel()
    {
        currentLevel = levels[levelIndex];
        GameEvents.RaiseLevelLoaded(currentLevel);
    }
}
