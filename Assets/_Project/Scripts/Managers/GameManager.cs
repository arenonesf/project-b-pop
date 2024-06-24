using System;
using FMOD.Studio;
using FMODUnity;
using ProjectBPop.Input;
using ProjectBPop.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private EventReference exitPortalEvent;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private SpawnPosition[] positions;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private EventReference backSoundReference;

    private EventInstance _backEventInstance;
    private GameObject _player;
    private Transform _playerCameraTransform;
    public int ProgressionNumber;
    public bool GamePaused { get; private set; }
    public bool SpawnMiddleHub = false;
    public static Action OnPlayerSet;

    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
        _backEventInstance = RuntimeManager.CreateInstance(backSoundReference);
    }
    
    private void OnEnable()
    {
        inputReader.PlayerPauseGameEvent += PauseGame;
        inputReader.PlayerResumeGameEvent += ResumeGame;
        SceneManager.sceneLoaded += SetPlayer;
    }

    private void OnDisable()
    {
        inputReader.PlayerPauseGameEvent -= PauseGame;
        inputReader.PlayerResumeGameEvent -= ResumeGame;
        SceneManager.sceneLoaded -= SetPlayer;
    }

    private void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inputReader.SetUI();
        GamePaused = true;
        Time.timeScale = 0f;
        UIManager.Instance.ShowPausedMenu();
    }

    public void ResumeGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (UIManager.Instance.OptionsMenuActiveInScene())
        {
            PlayBackButtonSound();
            UIManager.Instance.HideOptionsMenu();
            UIManager.Instance.ShowPausedMenu();
            return;
        }

        PlayBackButtonSound();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inputReader.SetGameplay();
        GamePaused = false;
        Time.timeScale = 1f;
        UIManager.Instance.HidePausedMenu();
    }

    public void UpdateProgressionNumber(int progressionNumber)
    {
        ProgressionNumber = progressionNumber;
    }

    private void SetPlayer(Scene scene, LoadSceneMode mode)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        screenFader = FindObjectOfType<ScreenFader>();
        _player.GetComponent<CharacterController>().enabled = false;
        SpawnPosition spawnPosition;
        var movement = _player.GetComponent<PlayerMovement>();

        
        if (scene.name == SceneReference.BlockHUBFINAL.ToString() && !SpawnMiddleHub)
        {
            spawnPosition = positions[0];
            movement.Rotated = true;
        }
        else if (scene.name == SceneReference.Zone1.ToString())
        {
            spawnPosition = positions[1];
            movement.Rotated = false;
            ExitPortalSound();
        }
        else if (scene.name == SceneReference.Zone2.ToString())
        {
            spawnPosition = positions[2];
            movement.Rotated = false;
            ExitPortalSound();
        }
        else if (scene.name == SceneReference.Zone3.ToString())
        {
            spawnPosition = positions[3];
            movement.Rotated = false;
            ExitPortalSound();
        }
        else
        {
            spawnPosition = positions[4];
            movement.Rotated = true;
            ExitPortalSound();
        }

        _player.transform.position = spawnPosition.Position;
        _player.GetComponentInChildren<Camera>().transform.rotation = spawnPosition.Rotation;
        _player.GetComponent<CharacterController>().enabled = true;
        screenFader.FadeOutImage();
        OnPlayerSet?.Invoke();
        inputReader.SetGameplay();
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    public Transform GetPlayerCameraTransform()
    {
        return _player.GetComponentInChildren<Camera>().gameObject.transform;
    }

    private void ExitPortalSound()
    {
        FMOD.Studio.EventInstance ExitPortalInstance = RuntimeManager.CreateInstance(exitPortalEvent);
        ExitPortalInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        ExitPortalInstance.start();
        ExitPortalInstance.release();
    }

    private void PlayBackButtonSound()
    {
        _backEventInstance.start();
    }
}
