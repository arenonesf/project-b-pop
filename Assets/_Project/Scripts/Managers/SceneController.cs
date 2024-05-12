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
        while (!load.isDone)
        {
            yield return null;
        }

        GameManager.Instance.GetPlayer().GetComponent<CharacterController>().enabled = false;
        GameManager.Instance.GetPlayer().transform.SetPositionAndRotation(spawnPosition.Position, spawnPosition.Rotation);
        GameManager.Instance.GetPlayer().GetComponent<CharacterController>().enabled = true;
    }
}
