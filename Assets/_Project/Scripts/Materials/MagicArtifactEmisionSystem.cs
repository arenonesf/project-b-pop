using ProjectBPop.Magic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArtifactEmisionSystem : MonoBehaviour
{
    private MagicArtifact _magicArtifact;
    private FSM _stateMachine;
    private BlackboardChangeEmision _blackboard;

    private void Awake()
    {
        _magicArtifact = GetComponent<MagicArtifact>();
        _blackboard = GetComponent<BlackboardChangeEmision>();

        _stateMachine = new FSM();

        var activateEmision = new StateActivateEmision(_blackboard);
        var deactivateEmision = new StateDeactivateEmision(_blackboard);
        var idleEmisionDeactivated = new StateIdleEmisionDeactivated(_blackboard);
        var idleEmisionActivated = new StateIdleEmisionActivated(_blackboard);

        At(idleEmisionActivated, deactivateEmision, HasNoMagic());
        At(idleEmisionDeactivated, activateEmision, HasMagic());
        At(deactivateEmision, activateEmision, HasMagic());
        At(activateEmision, deactivateEmision, HasNoMagic());
        At(deactivateEmision, idleEmisionDeactivated, MinValueToIdleDeactivated());
        At(activateEmision, idleEmisionActivated, MinValueToIdleActivated());

        if (_magicArtifact.Active)
        {
            _stateMachine.SetState(idleEmisionActivated);
        }
        else
        {
            _stateMachine.SetState(idleEmisionDeactivated);
        }

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> HasMagic() => () => _magicArtifact.Active;
        Func<bool> HasNoMagic() => () => !_magicArtifact.Active;
        Func<bool> MinValueToIdleActivated() => () => _blackboard.Intensity > _blackboard.MinActivatedIdleEmisionValue;
        Func<bool> MinValueToIdleDeactivated() => () => _blackboard.Intensity < _blackboard.MinDeactivatedIdleEmisionValue;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    } 
}
