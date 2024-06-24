using FMODUnity;
using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject perspectiveIcon;
    [SerializeField] private EventReference pauseGameEvent;
    [SerializeField] private GameObject optionsMenu;

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

    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public bool OptionsMenuActiveInScene()
    {
        return optionsMenu.activeInHierarchy;
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
