using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private int qualityLevel;

    private void Awake()
    {
        qualityLevel = PlayerPrefs.GetInt("QualityNumber", 3);
        dropdown.value = qualityLevel;
        AdjustQuality();
    }

    public void AdjustQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("QualityNumber", dropdown.value);
        qualityLevel = dropdown.value;
    }
}
