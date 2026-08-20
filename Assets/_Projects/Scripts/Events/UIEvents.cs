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
    public static event Action OnButtonClicked;
    public static event Action<string> OnLocaleChanged;

    public static void RaisePlayButtonClicked()
    {
        OnPlayButtonClicked?.Invoke();
        OnButtonClicked?.Invoke();
    }

    public static void RaiseCreditsButtonClicked()
    {
        OnCreditsButtonClicked?.Invoke();
        OnButtonClicked?.Invoke();
    }

    public static void RaiseSettingsButtonClicked()
    {
        OnSettingsButtonClicked?.Invoke();
        OnButtonClicked?.Invoke();
    }

    public static void RaiseLevelButtonClicked(string chapterName, int currentLevelIndex)
    {
        OnLevelButtonClicked?.Invoke(chapterName, currentLevelIndex);
        OnButtonClicked?.Invoke();
    }

    public static void RaiseMainMenuButtonClicked()
    {
        OnMainMenuButtonClicked?.Invoke();
        OnButtonClicked?.Invoke();
    }

    public static void RaiseRestartButtonClicked()
    {
        OnRestartButtonClicked?.Invoke();
        OnButtonClicked?.Invoke();
    }

    public static void RaiseNextLevelButtonClicked()
    {
        OnNextLevelButtonClicked?.Invoke();
        OnButtonClicked?.Invoke();
    }

    public static void RaiseButtonClicked()
    {
        OnButtonClicked?.Invoke();
    }

    public static void RaiseLocaleChange(string locale)
    {
        OnLocaleChanged?.Invoke(locale);
    }
}
