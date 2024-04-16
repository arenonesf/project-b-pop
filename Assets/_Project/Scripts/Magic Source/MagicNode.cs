using ProjectBPop.Player;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MonoBehaviour, IInteractable
    {
        [SerializeField] private SourceType nodeType;
        private bool _hasBeenPlaced;
        private PlayerMagic _playerMagicReference;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _playerMagicReference == null)
            {
                _playerMagicReference = other.GetComponent<PlayerMagic>();
            }
        }

        public void Interact()
        {
            if (_hasBeenPlaced)
            {
                SendSource();
            }
            else
            {
                PlaceSource();
            }
        }

        private void SendSource()
        {
            if (_playerMagicReference.GetMagicType() == SourceType.None) return;
            Debug.Log($"Source of type {nodeType} has been sent");
            _playerMagicReference.SetMagicType(nodeType);
        }

        private void PlaceSource()
        {
            if (_playerMagicReference.GetMagicType() != nodeType) return;
            Debug.Log($"Source of type {nodeType} has been placed");
            _playerMagicReference.SetMagicType(SourceType.None);
        }
    }
}
