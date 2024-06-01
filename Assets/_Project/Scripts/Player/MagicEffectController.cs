using System;
using UnityEngine;

public class MagicEffectController : MonoBehaviour
{
    [SerializeField] private Material fullScreenBlitMaterial;
    [SerializeField, ColorUsage(true, true)] private Color blueColor;
    [SerializeField, ColorUsage(true, true)] private Color redColor;
    [SerializeField, ColorUsage(true, true)] private Color greenColor;
    private static readonly int ScreenIntensity = Shader.PropertyToID("_ScreenIntensity");
    private static readonly int Color = Shader.PropertyToID("_Color");

    public void EnableFullScreenEffect(SourceType sourceType)
    {
        switch (sourceType)
        {
            case SourceType.Red:
                fullScreenBlitMaterial.SetColor(Color, redColor);
                break;
            case SourceType.Green:
                fullScreenBlitMaterial.SetColor(Color, greenColor);
                break;
            case SourceType.Blue:
                fullScreenBlitMaterial.SetColor(Color, blueColor);
                break;
            case SourceType.Colorless:
                break;
            case SourceType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sourceType), sourceType, null);
        }
        
        fullScreenBlitMaterial.SetFloat(ScreenIntensity, .1f);
    }

    public void DisableFullScreenEffect()
    {
        fullScreenBlitMaterial.SetFloat(ScreenIntensity, 0f);
    }
}
