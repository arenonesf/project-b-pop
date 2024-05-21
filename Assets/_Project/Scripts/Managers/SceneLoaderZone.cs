using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class SceneLoaderZone : MonoBehaviour
{
    [SerializeField] private SceneReference sceneReference;
    [SerializeField] private bool useSpawnPoint;
    [SerializeField] private SpawnPosition spawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (useSpawnPoint)
            {
                Debug.Log(GameManager.Instance.GetPlayer().transform.rotation);
                SceneController.Instance.LoadScene(sceneReference, spawnPosition);
            }
            else
            {
                SceneController.Instance.LoadScene(sceneReference);
            }
            
        }
    }
}
