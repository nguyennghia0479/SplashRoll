using UnityEngine;
using UnityEngine.Localization;

public class InfoUI : MonoBehaviour
{
    [Header("Localization Elements")]
    [SerializeField] protected string tableReference;
    [Space]
    [SerializeField] protected LocalizedString levelLocalizedString;
    [SerializeField] protected string levelKey;
    [Space]
    [SerializeField] protected LocalizedString movesLocalizedString;
    [SerializeField] protected string movesKey;
    [Space]
    [SerializeField] protected LocalizedString bestLocalizedString;
    [SerializeField] protected string bestKey;

    protected void Awake()
    {
        levelLocalizedString = new(tableReference, levelKey);
        movesLocalizedString = new(tableReference, movesKey);
        bestLocalizedString = new(tableReference, bestKey);
    }

    protected virtual void OnEnable()
    {
        levelLocalizedString.StringChanged += UpdateLevelText;
        movesLocalizedString.StringChanged += UpdateMovesText;
        bestLocalizedString.StringChanged += UpdateBestText;
    }

    protected virtual void OnDisable()
    {
        levelLocalizedString.StringChanged -= UpdateLevelText;
        movesLocalizedString.StringChanged -= UpdateMovesText;
        bestLocalizedString.StringChanged -= UpdateBestText;
    }

    protected virtual void UpdateLevelText(string value)
    {

    }

    protected virtual void UpdateMovesText(string value)
    {

    }

    protected virtual void UpdateBestText(string value)
    {

    }
}
