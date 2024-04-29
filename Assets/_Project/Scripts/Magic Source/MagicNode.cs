using System;
using ProjectBPop.Interfaces;
using System.Linq;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MonoBehaviour, IInteractable
    {
        [SerializeField] private SourceType nodeType;
        [SerializeField] public SourceType[] AcceptedTypes;
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
        public bool HasMagic => _hasMagic;
        private bool _isFirstTimePlaced;
        public Action OnCheckNode;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            ChangeMagicColor(nodeType, _hasMagic);
        }

        private void Start()
        {
            _playerReference = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
        }

        public void Interact()
        {
            
            if (!_playerReference) return;
            if (_playerReference.PlayerMagicSourceType == SourceType.None && _hasMagic)
            {
                SendMagicSource();
                UIManager.Instance.DisplayMagicMode();
            }
            else if(IsAcceptedType(_playerReference.PlayerMagicSourceType)  && !_hasMagic)
            {
                nodeType = _playerReference.PlayerMagicSourceType;
                RetrieveMagicSource();
                UIManager.Instance.HideMagicMode();
            }
            
            OnCheckNode?.Invoke();
        }

        private bool IsAcceptedType(SourceType playerType)
        {
            return AcceptedTypes.Any(type => type == playerType);
        }

        private void SendMagicSource()
        {
            _playerReference.SetMagicType(nodeType);
            _hasMagic = false;
            ChangeMagicColor(nodeType, _hasMagic);
        }
        
        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            _hasMagic = true;
            ChangeMagicColor(nodeType, _hasMagic);
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
