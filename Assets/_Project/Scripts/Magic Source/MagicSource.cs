using System;
using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public enum SourceType
    {
        Red,
        Green,
        Blue,
        Colorless,
        None
    }
    public class MagicSource : MonoBehaviour, IInteractable
    {
        [SerializeField] private SourceType sourceType;     
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
        private bool _magicSent;

        public static Action OnEnterTriggerArea;
        public static Action OnExitTriggerArea;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            ChangeMagicColor(sourceType, !_magicSent);
        }

        private void Start()
        {
            _playerReference = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!_magicSent)
                OnEnterTriggerArea?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnExitTriggerArea?.Invoke();
        }

        public void Interact()
        {
            if (!_playerReference) return;
            if (_playerReference.PlayerMagicSourceType == SourceType.None && !_magicSent)
            {
                SendMagicSource();
            }
            else if(_playerReference.PlayerMagicSourceType == sourceType && _magicSent)
            {
                RetrieveMagicSource();
            }
            
            UIManager.Instance.HideInteract();
        }

        private void SendMagicSource()
        {
            _playerReference.SetMagicType(sourceType);
            _magicSent = true;
            ChangeMagicColor(sourceType, !_magicSent);
            UIManager.Instance.DisplayMagicMode();
        }

        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            _magicSent = false;
            ChangeMagicColor(sourceType, !_magicSent);
            UIManager.Instance.HideMagicMode();
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