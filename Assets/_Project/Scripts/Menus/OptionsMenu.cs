using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private GameObject _optionsMenu;
    private void Awake()
    {
        _optionsMenu = GetComponentInChildren<Canvas>().gameObject;
        Hide();
    }

    public void Show()
    {
        _optionsMenu.SetActive(true);
    }

    public void Hide()
    {
        _optionsMenu.SetActive(false);
    }
}
