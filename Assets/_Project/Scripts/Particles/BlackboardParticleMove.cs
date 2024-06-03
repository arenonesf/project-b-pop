using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlackboardParticleMove : MonoBehaviour
{
    public float DiferenceToChangeState;
    public float Speed;
    public Transform HandPosition;
    public Transform MagicArtifactPosition;
    public Vector3 CurrentTarget;
    public bool Moving;
    public MagicArtifact MagicArtifact;
    public GameObject MagicParticles;

    private void Start()
    {
        HandPosition = GameManager.Instance.GetPlayer().GetComponentInChildren<HandPosition>().transform;
        MagicArtifactPosition = MagicArtifact.GetComponentInChildren<Canvas>().transform;
        MagicParticles = gameObject;
        MagicParticles.transform.position = MagicArtifactPosition.position;
        MagicParticles.GetComponent<ParticleSystem>().Stop();
        if (MagicArtifact.Active)
        {
            CurrentTarget = HandPosition.position;
        }
        else
        {
            CurrentTarget = MagicArtifactPosition.position;
            MagicParticles.gameObject.transform.SetParent(GameManager.Instance.GetPlayer().transform);
            MagicParticles.transform.position = HandPosition.position;
        }
        
    }

    private void OnEnable()
    {
        MagicArtifact.GiveMagicParticle += GiveMagicMoving;
        MagicArtifact.TakeMagicParticle += TakeMagicMoving;
    }

    private void OnDisable()
    {
        MagicArtifact.GiveMagicParticle -= GiveMagicMoving;
        MagicArtifact.TakeMagicParticle -= TakeMagicMoving;
        Destroy(MagicParticles);
    }

    private void GiveMagicMoving()
    {
        CurrentTarget = MagicArtifactPosition.position;
        MagicParticles.gameObject.transform.SetParent(MagicArtifact.transform);
        Moving = true;        
    }

    private void TakeMagicMoving()
    {
        CurrentTarget = HandPosition.position;
        MagicParticles.gameObject.transform.SetParent(GameManager.Instance.GetPlayer().transform);
        Moving = true;
    }
}
