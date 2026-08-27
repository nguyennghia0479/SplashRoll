using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler;
    [SerializeField] private float duration = .5f;

    private readonly int heightBonus = 1000; // use for mobile;

    private RectTransform fadeRect;
    private Vector2 leftPosition;
    private Vector2 centerPosition;
    private Vector2 rightPosition;

    private void Awake()
    {
        fadeRect = GetComponent<RectTransform>();

        SetupFadeRectTransform();
        SetupPosition();
        LoadFade();
    }

    private void SetupFadeRectTransform()
    {
        float screenWidth;
        float screenHeight;

        if (canvasScaler == null)
            canvasScaler = FindAnyObjectByType<CanvasScaler>();

        screenWidth = canvasScaler.referenceResolution.x;
        screenHeight = canvasScaler.referenceResolution.y;

#if UNITY_ANDROID
        screenHeight += heightBonus;
#endif

        fadeRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, screenWidth);
        fadeRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, screenHeight);
    }

    private void SetupPosition()
    {
        float width = fadeRect.rect.width;
        leftPosition = new(-width, 0);
        centerPosition = Vector2.zero;
        rightPosition = new(width, 0);
        fadeRect.anchoredPosition = centerPosition;
    }

    public void LoadFade(Action action = null)
    {
        StartCoroutine(Loading(action));
    }

    private IEnumerator Loading(Action action)
    {
        yield return LoadRoutine(fadeRect.anchoredPosition, centerPosition);

        action?.Invoke();
        yield return new WaitForSeconds(duration);

        yield return LoadRoutine(fadeRect.anchoredPosition, rightPosition);
        fadeRect.anchoredPosition = leftPosition;
    }

    private IEnumerator LoadRoutine(Vector2 startPosition, Vector2 targetPosition)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            fadeRect.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeRect.anchoredPosition = targetPosition;
    }
}
