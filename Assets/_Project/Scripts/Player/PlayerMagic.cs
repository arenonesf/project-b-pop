using System;
using ProjectBPop.Input;
using ProjectBPop.Magic;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMagic : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private float magicInteractionRange;
        [SerializeField] private LayerMask magicInteractionLayer;
        private bool _hasMagic;
        private SourceType _sourceType;
        private Camera _playerCamera;

        private void Awake()
        {
            _playerCamera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            _sourceType = SourceType.None;
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
            if (_hasMagic) return;
            var ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            if (!Physics.Raycast(ray, out RaycastHit hitInfo, magicInteractionRange, magicInteractionLayer.value)) return;
            Debug.Log("Pressed handle magic");
            _hasMagic = true;
        }

        private void HandleFireMagic()
        {
            if (!_hasMagic) return; 
            _hasMagic = false; 
            Debug.Log("Pressed fire magic");
        }

        public void SetMagicType(SourceType source)
        {
            _hasMagic = true;
            _sourceType = source;

        }

        public SourceType GetMagicType()
        {
            return _sourceType;
        }
    }
}
