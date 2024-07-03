using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject focusedElementGamepad;
    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        optionsMenu.SetActive(true);
        if (Gamepad.all.Count > 0) EventSystem.current.SetSelectedGameObject(focusedElementGamepad);
    }

    public void Hide()
    {
        optionsMenu.SetActive(false);
    }
}
