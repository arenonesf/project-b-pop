using ProjectBPop.Interfaces;
using UnityEngine;

namespace ProjectBPop.Puzzle
{ 
    public class NodeInteractor : MonoBehaviour
    {
        [SerializeField] private GameObject reactor;
        
        public void Solve()
        {
            reactor.GetComponent<IReact>().React();
        }
    }
}
