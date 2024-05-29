using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicSource : MagicArtifact, IInteractable
    {
        private PlayerInteract _playerInteract;
        
        private void OnEnable()
        {
            GameManager.OnPlayerSet += GetPlayer;
        }

        private void OnDisable()
        {
            GameManager.OnPlayerSet -= GetPlayer;
        }

        protected override void RetrieveMagic()
        {
            if (_playerInteract.PlayerMagicSourceType != type)
            {
                Debug.Log("DO NOTHING");
                return;
            }
            base.RetrieveMagic();
            Debug.Log("RETRIEVING MAGIC");
            _playerInteract.SetMagicType(SourceType.None);
            active = true;
        }

        protected override void SendMagic()
        {
            if (_playerInteract.PlayerMagicSourceType != SourceType.None)
            {
                Debug.Log("DK NOTHING");
                return;
            }
            base.SendMagic();
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
        
        private void GetPlayer()
        {
            _playerInteract = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
        }
    }
}