using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    public static Action <SpawnPosition> OnSceneLoaded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void LoadScene(SceneReference scene)
    {
        StartCoroutine(Load(scene));
    }

    public void LoadScene(SceneReference scene, SpawnPosition spawnPosition)
    {
        StartCoroutine(Load(scene, spawnPosition));
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
