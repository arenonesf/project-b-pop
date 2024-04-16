using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MonoBehaviour, IInteractable
    {
        [SerializeField] private SourceType nodeType;
        private PlayerInteract _playerReference;

        private void Awake()
        {
            _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        }

        public void Interact()
        {
            if (!_playerReference) return;
            if (_playerReference.PlayerMagicSourceType != nodeType) return;
            RetrieveMagicSource();
        }
        
        private void RetrieveMagicSource()
        {
            _playerReference.SetMagicType(SourceType.None);
            Debug.Log($"Magic Node has RETRIEVED magic of type {nodeType}");
        }
    }
}
