using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneReference playScene;
    [SerializeField] private SpawnPosition spawnPosition;
    [SerializeField] private bool useSpawnPoint;
    [SerializeField] private ScreenFader screenFader;
    private bool _clicked = false;
    private GameObject _mainMenu;

    private void Awake()
    {
        _mainMenu = GetComponentInChildren<Canvas>().gameObject;
    }

    public void Play()
    {
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
