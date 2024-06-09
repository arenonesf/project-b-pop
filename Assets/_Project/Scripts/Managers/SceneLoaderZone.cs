using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderZone : MonoBehaviour
{
    [SerializeField] private SceneReference sceneReference;
    [SerializeField] private bool useSpawnPoint;
    [SerializeField] private bool spawnMiddleHub;
    [SerializeField] private SpawnPosition spawnPosition;
    private GameObject _player;
    public Action OnEnterPortal;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindPlayer;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FindPlayer;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (useSpawnPoint)
            {             
                if (!spawnMiddleHub)
                {
                    GameManager.Instance.SpawnMiddleHub = false;
                }
                else
                {
                    GameManager.Instance.SpawnMiddleHub = true;
                }
                SceneController.Instance.LoadScene(sceneReference, spawnPosition);
                Debug.Log(GameManager.Instance.SpawnMiddleHub);
            }
            else
            {
                if (spawnMiddleHub)
                {
                    GameManager.Instance.SpawnMiddleHub = true;
                }
                else
                {
                    GameManager.Instance.SpawnMiddleHub = false;
                }
                SceneController.Instance.LoadScene(sceneReference);
            }
            OnEnterPortal?.Invoke();
        }
    }
    
    private void FindPlayer(Scene scene, LoadSceneMode mode)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
}
