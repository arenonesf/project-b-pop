using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParticleMoveMoving : IState
{
    private BlackboardParticleMove _blackboard;

    public StateParticleMoveMoving(BlackboardParticleMove blackboard)
    {
        _blackboard = blackboard;
    }

    public void OnEnter()
    {
        //nothing
    }

    public void OnUpdate()
    {
        if (_blackboard.CurrentTarget == Vector3.zero) return;
        _blackboard.MagicParticles.transform.position = Vector3.MoveTowards(_blackboard.MagicParticles.transform.position, _blackboard.CurrentTarget, _blackboard.Speed * Time.deltaTime);
    }

    public void OnExit()
    {
        _blackboard.Moving = false;

        if (_blackboard.CurrentTarget == _blackboard.HandPosition.position)
        {
            _blackboard.MagicParticles.transform.position = _blackboard.HandPosition.position;
            _blackboard.MagicParticles.transform.SetParent(_blackboard.HandPosition);
        }

        if (_blackboard.CurrentTarget == _blackboard.MagicArtifactPosition.position)
        {
            _blackboard.MagicParticles.transform.position = _blackboard.MagicArtifactPosition.position;
            _blackboard.MagicParticles.transform.SetParent(_blackboard.MagicArtifactPosition);
        }

        _blackboard.MagicParticles.GetComponent<ParticleSystem>().Stop();
    }
}
