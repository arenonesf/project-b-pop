using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicSource : MagicArtifact, IInteractable
    {
        private PlayerInteract _playerInteract;
        private MagicEffectController _magicEffectController;
        
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
            _magicEffectController.DisableFullScreenEffect();
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
            _magicEffectController.EnableFullScreenEffect(type);
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
            _magicEffectController = GameManager.Instance.GetPlayer().GetComponent<MagicEffectController>();
        }
    }
}