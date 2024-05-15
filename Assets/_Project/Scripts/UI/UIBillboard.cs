using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    private Transform _playerCameraTransform;
    
    private void Start()
    {
        _playerCameraTransform = GameManager.Instance.GetPlayerCameraTransform();
    }

    private void LateUpdate()
    {
        transform.forward = _playerCameraTransform.forward;
    }
}
