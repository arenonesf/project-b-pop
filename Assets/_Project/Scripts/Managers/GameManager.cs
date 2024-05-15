using ProjectBPop.Input;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public static GameManager Instance { get; private set; }
    private GameObject _player;
    private Transform _playerCameraTransform;
    public bool GamePaused { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        
        Instance = this;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCameraTransform = _player.GetComponentInChildren<Camera>().transform;
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

    public void PauseGame()
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

    #region Player Related Functions
    public GameObject GetPlayer()
    {
        return _player;
    }

    public Transform GetPlayerCameraTransform()
    {
        return _playerCameraTransform;
    }
    #endregion
}
