using System;
using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneReference playScene;
    [SerializeField] private SpawnPosition spawnPosition;
    [SerializeField] private bool useSpawnPoint;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private InputReader inputReader;
    
    private bool _clicked = false;
    private GameObject _mainMenu;

    private void Awake()
    {
        _mainMenu = GetComponentInChildren<Canvas>().gameObject;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Play()
    {
        gameObject.SetActive(false);
        if(!_clicked){
            if (useSpawnPoint && spawnPosition != null)
            {
                _clicked = true;
                Debug.Log("SpawnPoint");
                LoadGame();
            }
        }
        //SceneController.Instance.LoadScene(playScene);

    }

    private void LoadGame()
    {
        SceneController.Instance.LoadScene(playScene, spawnPosition);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void Show()
    {
        _mainMenu.SetActive(true);
    }

    public void Hide()
    {
        _mainMenu.SetActive(false);
    }
}
