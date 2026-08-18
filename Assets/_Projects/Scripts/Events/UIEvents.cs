using System;
using UnityEngine;

public static class UIEvents
{
    public static event Action OnPlayButtonClicked;
    public static event Action OnCreditsButtonClicked;
    public static event Action OnSettingsButtonClicked;
    public static event Action<string, int> OnLevelButtonClicked;
    public static event Action OnMainMenuButtonClicked;
    public static event Action OnRestartButtonClicked;
    public static event Action OnNextLevelButtonClicked;

    public static void RaisePlayButtonClicked()
    {
        OnPlayButtonClicked?.Invoke();
    }

    public static void RaiseCreditsButtonClicked()
    {
        OnCreditsButtonClicked?.Invoke();
    }

    public static void RaiseSettingsButtonClicked()
    {
        OnSettingsButtonClicked?.Invoke();
    }

    public static void RaiseLevelButtonClicked(string chapterName, int currentLevelIndex)
    {
        OnLevelButtonClicked?.Invoke(chapterName, currentLevelIndex);
    }

    public static void RaiseMainMenuButtonClicked()
    {
        OnMainMenuButtonClicked?.Invoke();
    }

    public static void RaiseRestartButtonClicked()
    {
        OnRestartButtonClicked?.Invoke();
    }

    public static void RaiseNextLevelButtonClicked()
    {
        OnNextLevelButtonClicked?.Invoke();
    }
}
