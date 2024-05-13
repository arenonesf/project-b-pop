using System;
using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Magic
{
    public class MagicNode : MagicArtifact, IInteractable
    {
        [SerializeField] private bool deactivateWhenSolved;
        private PlayerInteract _playerInteract;
        public Action OnCheckNode;
        public SourceType Type => type;
    
        private void Start()
        {
            _playerInteract = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
            if (active) OnCheckNode?.Invoke();
        }

        protected override void RetrieveMagic()
        {
            if(_playerInteract.PlayerMagicSourceType == SourceType.None) return;
            Debug.Log("RETRIEVING MAGIC");
            _playerInteract.SetMagicType(SourceType.None);
            active = true;
            OnCheckNode?.Invoke();
        }

        protected override void SendMagic()
        {
            if(deactivateWhenSolved) return;
            Debug.Log("SENDING MAGIC");
            _playerInteract.SetMagicType(type);
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
    }
}
