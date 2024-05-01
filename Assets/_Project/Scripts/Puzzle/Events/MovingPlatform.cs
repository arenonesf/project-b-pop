using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBPop.Interfaces;
using UnityEngine;

public class MovingPlatform : Mechanism
{
    [SerializeField] private List<Vector3> waypointPath;
    
    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    private void OnTriggerExit(Collider other)
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        if (Solved)
        {

        }
    }
    

    public override void Activate()
    {
        Solved = true;
    }

    public override void Deactivate()
    {
        Solved = false;
    }
}
