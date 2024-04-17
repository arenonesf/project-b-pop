using ProjectBPop.Interfaces;
using System;
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
        private PlayerInteract _playerReference;
        private bool _magicSent;
        public event Action<SourceType, bool> OnArtifactChangeColor;

        private void Awake()
        {
            _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
            OnArtifactChangeColor?.Invoke(sourceType, _magicSent);
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
        }

        private void SendMagicSource()
        {
            _playerReference.SetMagicType(sourceType);
            Debug.Log($"MAGIC SOURCE has SENT magic of type {sourceType}");
            _magicSent = true;
            OnArtifactChangeColor?.Invoke(sourceType, _magicSent);
            UIManager.Instance.DisplayMagicMode();
            VisionManager.Instance.EnableMeshRenderer();
        }

        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            Debug.Log($"MAGIC SOURCE has RETRIEVED magic of type {sourceType}");
            _magicSent = false;
            OnArtifactChangeColor?.Invoke(sourceType, _magicSent);
            UIManager.Instance.HideMagicMode();
            VisionManager.Instance.DisableMeshRenderer();
        }
    }
}