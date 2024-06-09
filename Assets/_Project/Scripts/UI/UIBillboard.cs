using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBillboard : MonoBehaviour
{
    private Transform _playerCameraTransform;

    private void OnEnable()
    {
        GameManager.OnPlayerSet += FindPlayer;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerSet -= FindPlayer;
    }


    private void OnDestroy()
    {
        _playerCameraTransform = null;
    }

    private void LateUpdate()
    {
        transform.LookAt(_playerCameraTransform);
    }

    private void FindPlayer()
    {
        _playerCameraTransform = GameManager.Instance.GetPlayerCameraTransform();
        Debug.Log(_playerCameraTransform);
    }
}
