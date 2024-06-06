using FMODUnity;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject perspectiveIcon;
    [SerializeField] private EventReference pauseGameEvent;
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
        FMOD.Studio.EventInstance pauseGameInstance = RuntimeManager.CreateInstance(pauseGameEvent);
        pauseGameInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        pauseGameInstance.start();
        pauseGameInstance.release();
    }

    public void HidePausedMenu()
    {
        pausedMenu.SetActive(false);
        FMOD.Studio.EventInstance pauseGameInstance = RuntimeManager.CreateInstance(pauseGameEvent);
        pauseGameInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        pauseGameInstance.start();
        pauseGameInstance.release();
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
