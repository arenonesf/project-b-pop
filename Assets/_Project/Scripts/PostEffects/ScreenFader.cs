using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fader;
    [SerializeField] private Image[] decals;
    [SerializeField] private float step;
    [SerializeField] private CanvasGroup canvasGroup;
    public Action OnFadeInComplete;

    public void FadeInImage()
    {
        StartCoroutine(FadeIn(0, 1));
    }

    public void FadeOutImage()
    {
        StartCoroutine(FadeOut(1, 0));
    }

    private IEnumerator FadeIn(float start, float end)
    {
        var alpha = start;
        fader.gameObject.SetActive(true);
        while (alpha <= end)
        {
            alpha += step;
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = end;

        yield return new WaitForSeconds(1f);
        if (alpha > 0f)
        {
            StartCoroutine(DisplayDecals());
        }
    }
    
    private IEnumerator FadeOut(float start, float end)
    {
        var alpha = start;
        fader.gameObject.SetActive(true);
        while (alpha >= end)
        {
            alpha -= step;
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = end;
        gameObject.SetActive(false);
    }

    private IEnumerator DisplayDecals()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowDecal(decals[0]));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ShowDecal(decals[1]));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowDecal(decals[2]));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(ShowDecal(decals[3]));
        OnFadeInComplete?.Invoke();
    }

    private IEnumerator ShowDecal(Image image)
    {
        var alpha = 0f;
        var target = 100f;
        
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        image.gameObject.SetActive(true);
        
        while (alpha < target)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            alpha += step;
            yield return null;
        }
        
        image.color = new Color(image.color.r, image.color.g, image.color.b, target);
    }
}
