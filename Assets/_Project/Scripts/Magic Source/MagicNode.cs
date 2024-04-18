using ProjectBPop.Interfaces;
using ProjectBPop.Puzzle;
using System;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MonoBehaviour, IInteractable
    {
        [SerializeField] private SourceType nodeType;
        [SerializeField] private SourceType[] acceptedTyped;
        [SerializeField] private Color redColorActivated;
        [SerializeField] private Color blueColorActivated;
        [SerializeField] private Color greenColorActivated;
        [SerializeField] private Color colorlessColorActivated;
        [SerializeField] private Color redColorDesactivated;
        [SerializeField] private Color blueColorDesactivated;
        [SerializeField] private Color greenColorDesactivated;
        [SerializeField] private Color colorlessColorDesactivated;
        private Material _material;
        private PlayerInteract _playerReference;
        private bool _hasMagic;
        private bool _isFirstTimePlaced;
        private NodeInteractor _nodeInteractor;
        private bool _solved;

        private void Awake()
        {
            _material = this.GetComponent<MeshRenderer>().material;
            _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
            _nodeInteractor = GetComponent<NodeInteractor>();
            ChangeMagicColor(nodeType, _hasMagic);
        }

        public void Interact()
        {
            if (!_playerReference) return;
            if (_playerReference.PlayerMagicSourceType == SourceType.None && _hasMagic)
            {
                SendMagicSource();
                UIManager.Instance.DisplayMagicMode();
                VisionManager.Instance.EnableMeshRenderer();
            }
            else if(_playerReference.PlayerMagicSourceType ==  && !_hasMagic)
            {
                RetrieveMagicSource();
                UIManager.Instance.HideMagicMode();
                if(!_solved)
                    VisionManager.Instance.DisableMeshRenderer();
            }
        }

        private bool IsAcceptedType()
        {
            
        }

        private void SendMagicSource()
        {
            _playerReference.SetMagicType(nodeType);
            Debug.Log($"MAGIC NODE has SENT magic of type {nodeType}");
            _hasMagic = false;
            ChangeMagicColor(nodeType, _hasMagic);
        }
        
        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            Debug.Log($"MAGIC NODE has RETRIEVED magic of type {nodeType}");
            _hasMagic = true;
            if (_isFirstTimePlaced) return;
            _isFirstTimePlaced = true;
            ChangeMagicColor(nodeType, _hasMagic);
            if (_solved) return;
            _nodeInteractor.Solve();
            _solved = true;
        }

        private void ChangeMagicColor(SourceType source, bool hasMagic)
        {
            switch (source)
            {
                case SourceType.Red:
                    if (hasMagic)
                        _material.color = redColorActivated;
                    else
                        _material.color = redColorDesactivated;
                    break;
                case SourceType.Blue:
                    if (hasMagic)
                        _material.color = blueColorActivated;
                    else
                        _material.color = blueColorDesactivated;
                    break;
                case SourceType.Green:
                    if (hasMagic)
                        _material.color = greenColorActivated;
                    else
                        _material.color = greenColorDesactivated;
                    break;
                default:
                    if (hasMagic)
                        _material.color = colorlessColorActivated;
                    else
                        _material.color = colorlessColorDesactivated;
                    break;
            }
        }
    }
}
