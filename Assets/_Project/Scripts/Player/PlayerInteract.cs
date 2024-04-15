using ProjectBPop.Input;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactionLayer;
    private Camera playerCamera;

    private void OnEnable()
    {
        inputReader.PlayerInteractEvent += InteractionRay;
    }

    private void OnDisable()
    {
        inputReader.PlayerInteractEvent -= InteractionRay;
    }

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    private void InteractionRay()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, interactionRange, interactionLayer.value)) return;
        var interactive = hitInfo.collider.GetComponent<ColorChanager>();
        interactive.Interact();
    }
}
