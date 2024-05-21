using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullScreen : MonoBehaviour
{
    [SerializeField] Toggle _fullScreenToggle;

    [SerializeField] TMP_Dropdown resolutionsDropdown;
    Resolution[] _resolutions;

    private void Awake()
    {
        if (Screen.fullScreen)
            _fullScreenToggle.isOn = true;
        else
            _fullScreenToggle.isOn = false;

        CheckResolution();
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    private void CheckResolution()
    {
        _resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && _resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolution;
        resolutionsDropdown.RefreshShownValue();
    }

    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
