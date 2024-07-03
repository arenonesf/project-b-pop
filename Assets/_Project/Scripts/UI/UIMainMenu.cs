using System;
using FMOD.Studio;
using FMODUnity;
using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private EventReference backSound;
    [SerializeField] private EventReference navigateSound;
    [SerializeField] private GameObject selectedForController;
    private EventInstance _backButtonInstance;
    private EventInstance _navigateButtonInstance;
    private GameObject _currentFocusedElement;

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
        _navigateButtonInstance = RuntimeManager.CreateInstance(navigateSound);
        if (!GamepadConnected()) return;
        EventSystem.current.SetSelectedGameObject(selectedForController);
        _currentFocusedElement = selectedForController;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != _currentFocusedElement)
        {
            PlayNavigateSound();
            _currentFocusedElement = EventSystem.current.currentSelectedGameObject;
        }
    }

    private void HandleUI()
    {
        if (!optionsMenu.activeInHierarchy) return;
        if(GamepadConnected()) EventSystem.current.SetSelectedGameObject(selectedForController);
        else EventSystem.current.SetSelectedGameObject(null);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        PlayBackSound();
    }

    private void PlayBackSound()
    {
        _backButtonInstance.start();
    }

    private void PlayNavigateSound()
    {
        _navigateButtonInstance.start();
    }

    private static bool GamepadConnected()
    {
        return Gamepad.all.Count > 0;
    }
}
