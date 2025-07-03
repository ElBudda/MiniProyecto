using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.5f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        // Ensure it starts black
        canvasGroup.alpha = 1;

        // Automatically fade in on scene load
        StartCoroutine(FadeInAtStart());
    }

    IEnumerator FadeInAtStart()
    {
        yield return new WaitForSeconds(0.1f);
        yield return FadeIn();
    }

    public IEnumerator FadeOut()
    {
        canvasGroup.blocksRaycasts = true;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}

