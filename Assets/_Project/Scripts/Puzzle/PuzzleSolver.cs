using System;
using System.Collections.Generic;
using ProjectBPop.Interfaces;
using ProjectBPop.Magic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] private List<MagicNode> nodes;
    [SerializeField] private Mechanism mechanism;
    [SerializeField] private bool deactivateAtSolve;
    public Action OnPuzzleComplete;
    
    private void OnEnable()
    {
        foreach (var node in nodes)
        {
            node.OnCheckNode += CheckSolution;
        }
    }

    private void OnDisable()
    {
        foreach (var node in nodes)
        {
            node.OnCheckNode -= CheckSolution;
        }
    }
    
    private void CheckSolution()
    {
        foreach (var node in nodes)
        {
            if (node.Active) continue;
            mechanism.Deactivate();
            return;
        }
        
        mechanism.Solved = true;
        mechanism.Activate();
        OnPuzzleComplete?.Invoke();
        
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
