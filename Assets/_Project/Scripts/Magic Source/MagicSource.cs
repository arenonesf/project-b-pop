using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicSource : MagicArtifact, IInteractable
    {
        private PlayerInteract _playerInteract;
    
        private void Start()
        {
            _playerInteract = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
        }

        protected override void RetrieveMagic()
        {
            if(_playerInteract.PlayerMagicSourceType == SourceType.None) return;
            Debug.Log("RETRIEVING MAGIC");
            _playerInteract.SetMagicType(SourceType.None);
            active = true;
        }

        protected override void SendMagic()
        {
            Debug.Log("SENDING MAGIC");
            _playerInteract.SetMagicType(type);
            active = false;
        }
    
        public void Interact()
        {
            if (active)
            {
                SendMagic();
            }
            else
            {
                RetrieveMagic();
            }
        }
    }
}