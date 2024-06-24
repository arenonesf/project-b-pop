using System.Collections;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private Material material;
    [SerializeField] private CreditsManager credits;
    private float step = 0.01f;
    private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");

    public void StartDissolve()
    {
        image.SetActive(true);
        StartCoroutine(nameof(Dissolve));
    }

    private IEnumerator Dissolve()
    {
        var start = 1f;
        var end = 0f;

        while (start > end)
        {
            start -= step;
            material.SetFloat(DissolveAmount, start);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        credits.EndCreditsAnimationEvent();
    }
}
