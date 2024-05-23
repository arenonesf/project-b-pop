using ProjectBPop.Input;
using System;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private SpawnPosition[] positions;
    private GameObject _player;
    private Transform _playerCameraTransform;
    public int ProgressionNumber;
    public bool GamePaused { get; private set; }
    public bool SpawnMiddleHub = false;

    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
    }
    
    private void OnEnable()
    {
        inputReader.PlayerPauseGameEvent += PauseGame;
        inputReader.PlayerResumeGameEvent += ResumeGame;
    }

    private void OnDisable()
    {
        inputReader.PlayerPauseGameEvent -= PauseGame;
        inputReader.PlayerResumeGameEvent -= ResumeGame;
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

    public SpawnPosition GetHubInitialSpawnPosition()
    {
        return positions[0];
    }

    public SpawnPosition GetHubMiddleSpawnPosition()
    {
        return positions[1];
    }

    public SpawnPosition GetFirstZoneSpawnPosition()
    {
        return positions[2];
    }
    
    public SpawnPosition GetSecondZoneSpawnPosition()
    {
        return positions[3];
    }
    
    public SpawnPosition GetThirdZoneSpawnPosition()
    {
        return positions[4];
    }
    
}
