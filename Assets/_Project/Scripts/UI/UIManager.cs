using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject perspectiveIcon;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void ShowPausedMenu()
    {
        pausedMenu.SetActive(true);
    }

    public void HidePausedMenu()
    {
        pausedMenu.SetActive(false);
    }

    public void DisplayPerspectiveIcon()
    {
        perspectiveIcon.SetActive(true);
    }

    public void HidePerspectiveIcon()
    {
        perspectiveIcon.SetActive(false);
    }
}
