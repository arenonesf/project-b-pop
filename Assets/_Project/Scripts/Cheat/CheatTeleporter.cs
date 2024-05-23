using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatTeleporter : MonoBehaviour
{
    [SerializeField] private List<SpawnPosition>  spawnPositionList;
    private GameObject _playerReference;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindPlayer;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FindPlayer;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Teleport(spawnPositionList[0]);
        }
    }

    private void Teleport(SpawnPosition spawnPosition)
    {
        _playerReference.GetComponent<CharacterController>().enabled = false;
        _playerReference.transform.SetPositionAndRotation(spawnPosition.Position, spawnPosition.Rotation);
        _playerReference.GetComponent<CharacterController>().enabled = true;
    }
    
    private void FindPlayer(Scene scene, LoadSceneMode mode)
    {
        _playerReference = GameObject.FindGameObjectWithTag("Player");
    }
}
