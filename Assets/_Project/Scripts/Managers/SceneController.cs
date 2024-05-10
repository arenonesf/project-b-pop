using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void LoadScene(SceneReference scene)
    {
        StartCoroutine(Load(scene));
    }

    private IEnumerator Load(SceneReference scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
        while (!load.isDone)
        {
            yield return null;
        }

    }
}
