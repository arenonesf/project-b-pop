using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMagic : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        private bool _hasMagic;
        
        // Start is called before the first frame update
        private void Start()
        {
            inputReader.PlayerGrabMagicEvent += HandleGrabMagic;
            inputReader.PlayerFireMagicEvent += HandleFireMagic;
        }

        private void OnDisable()
        {
            inputReader.PlayerGrabMagicEvent -= HandleGrabMagic;
            inputReader.PlayerFireMagicEvent -= HandleFireMagic;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void HandleGrabMagic()
        {
            Debug.Log("Pressed handle magic");
        }

        private void HandleFireMagic()
        {
            Debug.Log("Pressed fire magic");
        }
    }
}
