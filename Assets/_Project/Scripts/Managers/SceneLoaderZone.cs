using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderZone : MonoBehaviour
{
    [SerializeField] private SceneReference sceneReference;
    [SerializeField] private bool useSpawnPoint;
    [SerializeField] private SpawnPosition spawnPosition;
    private GameObject _player;
    
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
                Debug.Log(_player.transform.rotation);
                SceneController.Instance.LoadScene(sceneReference, spawnPosition);
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(SceneReference.MainMenu.ToString()) || SceneManager.GetActiveScene() != SceneManager.GetSceneByName(SceneReference.BlockHUBFINAL.ToString()))
                {
                    GameManager.Instance.SpawnMiddleHub = true;
                }
                else
                {
                    GameManager.Instance.SpawnMiddleHub = false;
                }
                Debug.Log(GameManager.Instance.SpawnMiddleHub);
            }
            else
            {
                SceneController.Instance.LoadScene(sceneReference);
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(SceneReference.MainMenu.ToString()) || SceneManager.GetActiveScene() != SceneManager.GetSceneByName(SceneReference.BlockHUBFINAL.ToString()))
                {
                    GameManager.Instance.SpawnMiddleHub = true;
                }
                else
                {
                    GameManager.Instance.SpawnMiddleHub = false;
                }
                Debug.Log(GameManager.Instance.SpawnMiddleHub);
            }
            
        }
    }
    
    private void FindPlayer(Scene scene, LoadSceneMode mode)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
}
