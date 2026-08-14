using System.Collections;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private float timeToChangeBackground = 20;
    [SerializeField] private float duration = 2;

    private SpriteRenderer spriteRenderer;
    private int currentIndex;
    private float backgroundChangeTimer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        currentIndex = Random.Range(0, colors.Length);
        spriteRenderer.color = colors[currentIndex];
    }

    private void Update()
    {
        backgroundChangeTimer += Time.deltaTime;
        if (backgroundChangeTimer >= timeToChangeBackground)
        {
            backgroundChangeTimer = 0;
            ChangeBackground();
        }
    }

    private void ChangeBackground()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, colors.Length);
        } while (currentIndex == randomIndex);

        currentIndex = randomIndex;
        StartCoroutine(ChangeBackgroundRoutine(colors[currentIndex]));
    }

    private IEnumerator ChangeBackgroundRoutine(Color targetColor)
    {
        float elapseTime = 0;
        Color startColor = spriteRenderer.color;

        while (elapseTime < duration)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapseTime / duration);
            elapseTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }
}
