using System;
using ProjectBPop.Input;
using ProjectBPop.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private SpawnPosition[] positions;
    private GameObject _player;
    private Transform _playerCameraTransform;
    public int ProgressionNumber;
    public bool GamePaused { get; private set; }
    public bool SpawnMiddleHub = false;
    public static Action OnPlayerSet;

    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
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
        SpawnPosition spawnPosition;
        var movement = _player.GetComponent<PlayerMovement>();
        
        if (scene.name == SceneReference.BlockHUBFINAL.ToString())
        {
            spawnPosition = positions[0];
            movement.Rotated = true;
        }
        else if (scene.name == SceneReference.Zone1.ToString())
        {
            spawnPosition = positions[1];
            movement.Rotated = false;
        }
        else if (scene.name == SceneReference.Zone2.ToString())
        {
            spawnPosition = positions[2];
            movement.Rotated = false;
        }
        else if (scene.name == SceneReference.Zone3.ToString())
        {
            spawnPosition = positions[3];
            movement.Rotated = false;
        }
        else
        {
            spawnPosition = positions[4];
            movement.Rotated = true;
        }

        _player.transform.position = spawnPosition.Position;
        _player.GetComponentInChildren<Camera>().transform.rotation = spawnPosition.Rotation;
        OnPlayerSet?.Invoke();
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
