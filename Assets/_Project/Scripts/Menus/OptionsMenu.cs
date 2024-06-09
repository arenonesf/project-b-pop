using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        optionsMenu.SetActive(true);
    }

    public void Hide()
    {
        optionsMenu.SetActive(false);
    }
}
