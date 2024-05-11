using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    [SerializeField] private Transform currentCheckpoint;
    private GameObject _player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        _player = GameManager.Instance.GetPlayer();
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.SetPositionAndRotation(currentCheckpoint.position, currentCheckpoint.rotation);
        _player.GetComponent<CharacterController>().enabled = true;
    }

    public void ChangeCurrentCheckpoint(Transform newCheckpoint)
    {
        Debug.Log("Updated");
        currentCheckpoint = newCheckpoint;
    }
}
