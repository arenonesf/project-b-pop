using System;
using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private SpawnPosition[] positions;
    private GameObject _player;
    private Transform _playerCameraTransform;
    public bool GamePaused { get; private set; }
    public static Action OnLoadScene;

    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
    }
    
    private void OnEnable()
    {
        inputReader.PlayerPauseGameEvent += PauseGame;
        inputReader.PlayerResumeGameEvent += ResumeGame;
        SceneController.OnSceneLoaded += AssignPlayerAndCamera;
    }

    private void OnDisable()
    {
        inputReader.PlayerPauseGameEvent -= PauseGame;
        inputReader.PlayerResumeGameEvent -= ResumeGame;
        SceneController.OnSceneLoaded -= AssignPlayerAndCamera;
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
    
    private void AssignPlayerAndCamera(SpawnPosition spawnPosition)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCameraTransform = _player.GetComponentInChildren<Camera>().transform;
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.localPosition = spawnPosition.Position;
        _player.transform.Rotate(Vector3.up, spawnPosition.Rotation.y-_player.transform.rotation.y ,Space.World);
        Debug.Log(_player.transform.localRotation);
        OnLoadScene?.Invoke();
        _player.GetComponent<CharacterController>().enabled = true;
    }
}
