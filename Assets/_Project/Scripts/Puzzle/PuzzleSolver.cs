using System.Collections.Generic;
using ProjectBPop.Interfaces;
using ProjectBPop.Magic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] private List<MagicNode> nodes;
    [SerializeField] private Mechanism mechanism;
    [SerializeField] private bool deactivateAtSolve;
    
    private void OnEnable()
    {
        if (nodes.Count > 1)
        {
            foreach (var node in nodes)
            {
                node.OnCheckNode += CheckSolution;
            }
        }
        else
        {
            nodes[0].OnCheckNode += CheckSolution;
        }
    }

    private void OnDisable()
    {
        if (nodes.Count > 1)
        {
            foreach (var node in nodes)
            {
                node.OnCheckNode -= CheckSolution;
            }
        }
        else
        {
            nodes[0].OnCheckNode -= CheckSolution;
        }
    }
    
    private void CheckSolution()
    {
        if (nodes.Count > 1)
        {
            foreach (var node in nodes)
            {
                if (!node.Active)
                {
                    if(mechanism.Solved) mechanism.Deactivate();
                    return;
                }
            }
        }
        else
        {
            if (!nodes[0].Active)
            {
                if(mechanism.Solved) mechanism.Deactivate();
                return;
            }
        }
        
        mechanism.Solved = true;
        mechanism.Activate();
        if (deactivateAtSolve)
        {
            DisableNodes();
        }
    }

    private void DisableNodes()
    {
        foreach (var node in nodes)
        {
            node.SetNodeInactive();
        }
    }
}
