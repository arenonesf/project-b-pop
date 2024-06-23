using System;
using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MagicArtifact, IInteractable
    {
        [SerializeField] private bool deactivateWhenSolved;
        [SerializeField] private GameObject pathParticle;
        [SerializeField] private GameObject pickParticle;
        private PlayerInteract _playerInteract;
        private MagicEffectController _magicEffectController;
        public Action OnCheckNode;
        public SourceType Type => type;
    
        private void Start()
        {
            if (active) OnCheckNode?.Invoke();
        }

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
            if (_playerInteract.PlayerMagicSourceType != type && acceptType != SourceType.Colorless)
            {
                Debug.Log("CAN'T RETRIEVE MAGIC");
                _playerInteract.ResetInteraction();
                return;
            }
            Debug.Log("RETRIEVING MAGIC");
            base.RetrieveMagic();
            _playerInteract.SetMagicType(SourceType.None);
            _magicEffectController.DisableFullScreenEffect();
            pickParticle.SetActive(true);
            StartCoroutine(nameof(DeactivateParticle));
            active = true;
            OnCheckNode?.Invoke();
        }

        protected override void SendMagic()
        {
            if (_playerInteract.PlayerMagicSourceType != SourceType.None && deactivateWhenSolved)
            {
                Debug.Log("CAN'T SEND MAGIC");
                _playerInteract.ResetInteraction();
                return;
            }
            Debug.Log("SENDING MAGIC");
            base.SendMagic();
            _playerInteract.SetMagicType(type);
            _magicEffectController.EnableFullScreenEffect(type);
            pathParticle.SetActive(true);
            active = false;
            OnCheckNode?.Invoke();
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

        public void SetNodeInactive()
        {
            deactivateWhenSolved = true;
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
