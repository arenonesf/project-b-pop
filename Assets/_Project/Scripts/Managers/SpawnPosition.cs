using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPosition", menuName = "SpawnPosition", order = 0)]

public class SpawnPosition : ScriptableObject
{
    public Vector3 Position;
    public Quaternion Rotation;
}
