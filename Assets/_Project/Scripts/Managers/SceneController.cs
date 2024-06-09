using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    public static Action <SpawnPosition> OnSceneLoaded;
    [SerializeField] private ScreenFader screenFader;
    private SceneReference _scene;
    private SpawnPosition _spawn;
    private bool _shouldspawn = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        screenFader = FindObjectOfType<ScreenFader>();
        Instance = this;
    }

    public void LoadScene(SceneReference scene)
    {
        screenFader.OnFadeInComplete += CoroutineLoadScene;
        _scene = scene;
        _shouldspawn = false;
        screenFader.gameObject.SetActive(true);
        screenFader.FadeInImage();
    }

    public void LoadScene(SceneReference scene, SpawnPosition spawnPosition)
    {
        screenFader.OnFadeInComplete += CoroutineLoadScene;
        _scene = scene;
        _spawn = spawnPosition;
        _shouldspawn = true;
        screenFader.gameObject.SetActive(true);
        screenFader.FadeInImage();
    }

    private void CoroutineLoadScene()
    {
        if (_shouldspawn) StartCoroutine(Load(_scene));
        else StartCoroutine(Load(_scene, _spawn));
    }

    private IEnumerator Load(SceneReference scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
        
        while (!load.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator Load(SceneReference scene, SpawnPosition spawnPosition)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
        load.completed += (x) => { OnSceneLoaded?.Invoke(spawnPosition); };

        while (!load.isDone)
        {
            yield return null;
        }
    }
}
