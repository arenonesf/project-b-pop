using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Puzzle
{
    public enum RiddleType
    {
        TargetAppear,
        TargetDisappear,
        TargetMove,
        TargetStop
    }
    
    public class NodeInteractor : MonoBehaviour
    {
        [SerializeField] private GameObject reactor;
        private IReact _reactorReference;
    
        // Start is called before the first frame update
        private void Start()
        {
            _reactorReference = GetComponent<IReact>();
        }

        public void Solve()
        {
            _reactorReference.React();
        }
    }
}
