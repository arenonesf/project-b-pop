using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;
    
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
}
