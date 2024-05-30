using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardDoor : MonoBehaviour
{
    public float DiferenceToChangeState;
    public float Speed;
    public Transform OpenPosition;
    public Transform ClosePosition;
    public bool Moving;
    public Door Door;
}
