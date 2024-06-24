using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCredits : MonoBehaviour
{
    public GameObject _gameManager;

    private void OnTriggerEnter(Collider other)
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        RemoveFromDontDestroyOnLoad();
    }

    void RemoveFromDontDestroyOnLoad()
    {
        // Crear una nueva escena temporal
        Scene tempScene = SceneManager.CreateScene("TempScene");

        // Mover el objeto a la nueva escena
        SceneManager.MoveGameObjectToScene(_gameManager, tempScene);
    }
}
