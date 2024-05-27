using ProjectBPop.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectBPop.Magic
{
    public class MagicSource : MagicArtifact, IInteractable
    {
        private PlayerInteract _playerInteract;
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += FindPlayer;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= FindPlayer;
        }

        protected override void RetrieveMagic()
        {
            if(_playerInteract.PlayerMagicSourceType != type) return;
            base.RetrieveMagic();
            Debug.Log("RETRIEVING MAGIC");
            _playerInteract.SetMagicType(SourceType.None);
            active = true;
        }

        protected override void SendMagic()
        {
            if (_playerInteract.PlayerMagicSourceType != SourceType.None) return;
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
        
        private void FindPlayer(Scene scene, LoadSceneMode mode)
        {
            _playerInteract = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        }
    }
}