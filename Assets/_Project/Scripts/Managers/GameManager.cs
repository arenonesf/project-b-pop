using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject _player;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        
        Instance = this;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    public Transform GetPlayerCameraTransform()
    {
        Debug.Log(_player.GetComponentInChildren<Camera>().transform.name);
        return _player.GetComponentInChildren<Camera>().transform;
    }
}
