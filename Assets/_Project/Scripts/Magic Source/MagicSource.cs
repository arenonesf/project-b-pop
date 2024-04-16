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

        private void Awake()
        {
            _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        }
        
        public void Interact()
        {
            if (!_playerReference) return;
            if (_playerReference.PlayerMagicSourceType == SourceType.None)
            {
                SendMagicSource();
            }
            else if(_playerReference.PlayerMagicSourceType == sourceType)
            {
                RetrieveMagicSource();
            }
        }

        private void SendMagicSource()
        {
            _playerReference.SetMagicType(sourceType);
            Debug.Log($"MAGIC SOURCE has SENT magic of type {sourceType}");
        }

        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            Debug.Log($"MAGIC SOURCE has RETRIEVED magic of type {sourceType}");
        }
    }
}