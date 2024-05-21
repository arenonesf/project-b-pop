using ProjectBPop.Input;
using ProjectBPop.Player;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private InputReader inputReader;
    
    private GameObject _player;
    private Transform _playerCameraTransform;
    public bool GamePaused { get; private set; }

    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
 
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCameraTransform = _player.GetComponentInChildren<Camera>().transform;
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

    #region Player Related Functions
    public GameObject GetPlayer()
    {
        return Instance._player;
    }

    public Transform GetPlayerCameraTransform()
    {
        return Instance._playerCameraTransform;
    }
    #endregion

    private void AssignPlayerAndCamera(SpawnPosition spawnPosition)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCameraTransform = _player.GetComponentInChildren<Camera>().transform;
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.localPosition = spawnPosition.Position;
        _player.transform.Rotate(Vector3.up, spawnPosition.Rotation.y-_player.transform.rotation.y ,Space.World);
        Debug.Log(_player.transform.localRotation);
        _player.GetComponent<CharacterController>().enabled = true;
    } 
}
