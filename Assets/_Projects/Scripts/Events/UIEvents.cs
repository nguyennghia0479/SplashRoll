using System;
using UnityEngine;

public static class UIEvents
{
    public static event Action OnPlayButtonClicked;
    public static event Action OnCreditsButtonClicked;
    public static event Action OnSettingsButtonClicked;

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
}
