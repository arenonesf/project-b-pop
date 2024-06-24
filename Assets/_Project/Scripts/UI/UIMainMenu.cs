using FMOD.Studio;
using FMODUnity;
using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private EventReference backSound;
    private EventInstance _backButtonInstance;

    private void OnEnable()
    {
        inputReader.PlayerResumeGameEvent += HandleUI;
    }

    private void OnDisable()
    {
        inputReader.PlayerResumeGameEvent -= HandleUI;
    }

    private void Awake()
    {
        inputReader.SetUI();
        _backButtonInstance = RuntimeManager.CreateInstance(backSound);
    }

    private void HandleUI()
    {
        if (!optionsMenu.activeInHierarchy) return;
        EventSystem.current.SetSelectedGameObject(null);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        PlayBackSound();
    }

    private void PlayBackSound()
    {
        _backButtonInstance.start();
    }
}
