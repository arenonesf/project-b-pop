using System;
using System.Collections.Generic;
using ProjectBPop.Interfaces;
using ProjectBPop.Magic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] private List<MagicNode> nodes;
    [SerializeField] private Mechanism mechanism;

    private void Start()
    {
        mechanism.Activate();
    }

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
                if (!node.HasMagic)
                {
                    Debug.Log("A node has no magic");
                    if(mechanism.Solved) mechanism.Deactivate();
                    return;
                }
            }
        }
        else
        {
            if (!nodes[0].HasMagic)
            {
                Debug.Log("A node has no magic");
                if(mechanism.Solved) mechanism.Deactivate();
                return;
            }
        }
        
        Debug.Log("SOLVED");
        mechanism.Solved = true;
        mechanism.Activate();
    }
}
