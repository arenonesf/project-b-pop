using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicSource : MagicArtifact, IInteractable
    {
        [SerializeField] private GameObject pathParticle;
        [SerializeField] private GameObject pickParticle;
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
                return;
            }
            base.RetrieveMagic();
            _playerInteract.SetMagicType(SourceType.None);
            _magicEffectController.DisableFullScreenEffect();
            pickParticle.SetActive(true);
            StartCoroutine(nameof(DeactivateParticle));
            active = true;
        }

        protected override void SendMagic()
        {
            if (_playerInteract.PlayerMagicSourceType != SourceType.None)
            {
                return;
            }
            base.SendMagic();
            _playerInteract.SetMagicType(type);
            _magicEffectController.EnableFullScreenEffect(type);
            pathParticle.SetActive(true);
            active = false;
        }
    
        public void Interact()
        {
            if (active)
            {
                SendMagic();
            }
            else if(_playerInteract.PlayerMagicSourceType != SourceType.None)
            {
                RetrieveMagic();
            }
        }
        
        private void GetPlayer()
        {
            _playerInteract = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
            _magicEffectController = GameManager.Instance.GetPlayer().GetComponent<MagicEffectController>();
        }

        private IEnumerator DeactivateParticle()
        {
            yield return new WaitForSeconds(0.5f);
            pickParticle.SetActive(false);
            pathParticle.SetActive(false);
        }
    }
}