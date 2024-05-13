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
    
    }

    private void OnEnable() {
        _player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(_player);
        Debug.Log("HOLA");
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
