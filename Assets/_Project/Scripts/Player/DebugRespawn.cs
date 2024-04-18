using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class DebugRespawn : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private Vector3 position;

        private void OnEnable()
        {
            playerInput.PlayerRespawnEvent += Respawn;
        }

        private void OnDisable()
        {
            playerInput.PlayerRespawnEvent -= Respawn;
        }

        private void Respawn()
        {
            transform.position = position;
            Physics.SyncTransforms();
        }
    }
}
