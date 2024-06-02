using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParticleMoveIdle : IState
{
    private BlackboardParticleMove _blackboard;

    public StateParticleMoveIdle(BlackboardParticleMove blackboard)
    {
        _blackboard = blackboard;
    }

    public void OnEnter()
    {
        //Nothing
    }

    public void OnUpdate()
    {
        //Nothing
    }

    public void OnExit()
    {
        _blackboard.MagicParticles.GetComponent<ParticleSystem>().Play();
    }
}
