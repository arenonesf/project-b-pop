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
        private PlayerInteract _playerReference;
        private bool _magicSent;

        private void Awake()
        {
            _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
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
            UIManager.Instance.DisplayMagicMode();
            VisionManager.Instance.SetSpecialMaterial();
        }

        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            Debug.Log($"MAGIC SOURCE has RETRIEVED magic of type {sourceType}");
            _magicSent = false;
            UIManager.Instance.HideMagicMode();
            VisionManager.Instance.HideObjects();
        }
    }
}