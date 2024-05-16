using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneReference playScene;
    [SerializeField] private SpawnPosition spawnPosition;
    [SerializeField] private bool useSpawnPoint;

    public void Play()
    {
        if (useSpawnPoint && spawnPosition != null)
        {
            Debug.Log("SpawnPoint");
            SceneController.Instance.LoadScene(playScene, spawnPosition);
        }
        SceneController.Instance.LoadScene(playScene);

    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

}
