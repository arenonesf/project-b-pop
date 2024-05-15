using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    
    private void LateUpdate()
    {
        transform.forward = playerCameraTransform.forward;
    }
}
