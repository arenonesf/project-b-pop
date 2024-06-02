using System;
using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MagicArtifact, IInteractable
    {
        [SerializeField] private bool deactivateWhenSolved;
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
                Debug.Log("DO NOTHING");
                return;
            }
            base.RetrieveMagic();
            Debug.Log("RETRIEVING MAGIC");
            _playerInteract.SetMagicType(SourceType.None);
            _magicEffectController.DisableFullScreenEffect();
            active = true;
            OnCheckNode?.Invoke();
        }

        protected override void SendMagic()
        {
            if(deactivateWhenSolved) return;
            if (_playerInteract.PlayerMagicSourceType != SourceType.None) return;
            base.SendMagic();
            Debug.Log("SENDING MAGIC");
            _playerInteract.SetMagicType(type);
            _magicEffectController.EnableFullScreenEffect(type);
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
    }
}
