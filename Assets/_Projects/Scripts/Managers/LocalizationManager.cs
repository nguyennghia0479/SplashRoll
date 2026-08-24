using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    private void OnEnable()
    {
        UIEvents.OnLocaleChanged += HandleLocaleChanaged;
    }

    private void OnDisable()
    {
        UIEvents.OnLocaleChanged -= HandleLocaleChanaged;
    }

    private void HandleLocaleChanaged(string locale)
    {
        StartCoroutine(ChangeLocaleRoutine(locale));
    }

    private IEnumerator ChangeLocaleRoutine(string locale)
    {
        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(locale);
    }
}
