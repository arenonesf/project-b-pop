using ProjectBPop.Interfaces;
using ProjectBPop.Puzzle;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MonoBehaviour, IInteractable
    {
        [SerializeField] private SourceType nodeType;
        private PlayerInteract _playerReference;
        private bool _hasMagic;
        private bool _isFirstTimePlaced;
        private NodeInteractor _nodeInteractor;

        private void Awake()
        {
            _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
            _nodeInteractor = GetComponent<NodeInteractor>();
        }

        public void Interact()
        {
            if (!_playerReference) return;
            if (_playerReference.PlayerMagicSourceType == SourceType.None && _hasMagic)
            {
                SendMagicSource();
                UIManager.Instance.DisplayMagicMode();
                VisionManager.Instance.SetSpecialMaterial();
            }
            else if(_playerReference.PlayerMagicSourceType == nodeType && !_hasMagic)
            {
                RetrieveMagicSource();
                UIManager.Instance.HideMagicMode();
                VisionManager.Instance.HideObjects();
            }
        }

        private void SendMagicSource()
        {
            _playerReference.SetMagicType(nodeType);
            Debug.Log($"MAGIC NODE has SENT magic of type {nodeType}");
            _hasMagic = false;
        }
        
        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            Debug.Log($"MAGIC NODE has RETRIEVED magic of type {nodeType}");
            _hasMagic = true;
            if (_isFirstTimePlaced) return;
            _isFirstTimePlaced = true;
            _nodeInteractor.Solve();
        }
    }
}
