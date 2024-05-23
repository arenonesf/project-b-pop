using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    private Transform _playerCameraTransform;

    private void OnEnable()
    {
        GameManager.OnLoadScene += GetPlayer;
    }

    private void OnDisable()
    {
        GameManager.OnLoadScene -= GetPlayer;
    }
    
    private void LateUpdate()
    {
        if (!_playerCameraTransform) return;
        transform.forward = _playerCameraTransform.forward;
    }

    private void GetPlayer()
    {
        _playerCameraTransform = GameManager.Instance.GetPlayerCameraTransform();
    }
}
