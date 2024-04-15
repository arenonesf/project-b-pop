using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMagic : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private float magicInteractionRange;
        [SerializeField] private LayerMask magicInteractionLayer;
        private bool _hasMagic;
        private Camera playerCamera;


        private void Awake()
        {
            playerCamera = GetComponentInChildren<Camera>();
        }

        private void OnEnable()
        {
            inputReader.PlayerGrabMagicEvent += HandleGrabMagic;
            inputReader.PlayerFireMagicEvent += HandleFireMagic;
        }

        private void OnDisable()
        {
            inputReader.PlayerGrabMagicEvent -= HandleGrabMagic;
            inputReader.PlayerFireMagicEvent -= HandleFireMagic;
        }

        private void HandleGrabMagic()
        {
            var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));  
            if (!_hasMagic)
            {
                if (!Physics.Raycast(ray, out RaycastHit hitInfo, magicInteractionRange, magicInteractionLayer.value)) return;
                Debug.Log("Pressed handle magic");
                _hasMagic = true;
            }       
        }

        private void HandleFireMagic()
        {
            if (_hasMagic)
            {
                _hasMagic = false;
                Debug.Log("Pressed fire magic");
            }
            
        }
    }
}
