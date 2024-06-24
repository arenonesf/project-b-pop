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
    }

    public void Respawn()
    {
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.SetPositionAndRotation(currentCheckpoint.position, currentCheckpoint.rotation);
        _player.GetComponent<CharacterController>().enabled = true;
    }

    public void ChangeCurrentCheckpoint(Transform newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
}
