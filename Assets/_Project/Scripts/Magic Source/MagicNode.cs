using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MonoBehaviour, IInteractable
    {
        [SerializeField] private SourceType nodeType;
        private PlayerInteract _playerReference;
        private bool _magicHasBeenPlaced;

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
            else if(_playerReference.PlayerMagicSourceType == nodeType)
            {
                RetrieveMagicSource();
            }
        }

        private void SendMagicSource()
        {
            _playerReference.SetMagicType(nodeType);
            Debug.Log($"MAGIC NODE has SENT magic of type {nodeType}");
            _magicHasBeenPlaced = false;
        }
        
        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            Debug.Log($"MAGIC NODE has RETRIEVED magic of type {nodeType}");
            _magicHasBeenPlaced = true;
        }
    }
}
