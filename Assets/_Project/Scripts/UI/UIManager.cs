using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject perspectiveIcon;
    [SerializeField] private EventReference pauseGameEvent;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private EventReference navigateSound;

    private GameObject _currentFocusedElement;
    private EventInstance _navigateButtonInstance;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
        _navigateButtonInstance = RuntimeManager.CreateInstance(navigateSound);
    }
    
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != _currentFocusedElement)
        {
            PlayNavigateSound();
            _currentFocusedElement = EventSystem.current.currentSelectedGameObject;
        }
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

    private void PlayNavigateSound()
    {
        _navigateButtonInstance.start();
    }

}
