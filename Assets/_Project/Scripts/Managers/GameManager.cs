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

        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
