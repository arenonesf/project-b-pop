using ProjectBPop.Player;
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
        private PlayerMagic _playerReference;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerReference = other.GetComponent<PlayerMagic>();
            }
        }

        public void Interact()
        {
            if (_playerReference)
            {
                _playerReference.SetMagicType(sourceType);
            }
        }
    }
}