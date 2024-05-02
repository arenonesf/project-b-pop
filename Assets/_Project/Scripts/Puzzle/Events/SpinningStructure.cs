using ProjectBPop.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpinningStructure : Mechanism
{
    [SerializeField] private float rotSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private Transform rotatingObject;
    private float _degrees;
    private bool _finishedRotation;

    private void Update()
    {
        if (!Solved) return;
        if (_finishedRotation) return;       
        Rotate();
    }

    private void Rotate()
    {
        Vector3 dir = target.position - rotatingObject.position;
        dir.Normalize();
        Quaternion rot = Quaternion.Slerp(rotatingObject.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
        rot.x = 0;
        rot.z = 0;
        rotatingObject.rotation = rot;
        Debug.Log(Mathf.Abs(Vector3.Dot(rotatingObject.right, dir)));
        if (Mathf.Abs(Vector3.Dot(rotatingObject.right, dir)) <= 0.1)
        {
            _finishedRotation = true;
        }
    }

    public override void Activate()
    {
        Solved = true;
    }

    public override void Deactivate()
    {

    }
}
