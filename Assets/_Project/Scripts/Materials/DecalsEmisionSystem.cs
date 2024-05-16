using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalsEmisionSystem : MonoBehaviour
{
    private FSM _stateMachine;
    private BlackboardChangeEmision _blackboard;
    private PlayerInteract _playerReference;

    private void Start()
    {
        _blackboard = GetComponent<BlackboardChangeEmision>();
        _playerReference = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();

        _stateMachine = new FSM();

        var activateEmision = new StateActivateEmision(_blackboard);
        var deactivateEmision = new StateDeactivateEmision(_blackboard);
        var idleEmisionDeactivated = new StateIdleEmisionDeactivated(_blackboard);
        var idleEmisionActivated = new StateIdleEmisionActivated(_blackboard);

        At(idleEmisionActivated, deactivateEmision, MagicVisionDeactivated());
        At(idleEmisionDeactivated, activateEmision, MagicVisionActivated());
        At(deactivateEmision, activateEmision, MagicVisionActivated());
        At(activateEmision, deactivateEmision, MagicVisionDeactivated());
        At(deactivateEmision, idleEmisionDeactivated, MinValueToIdleDeactivated());
        At(activateEmision, idleEmisionActivated, MinValueToIdleActivated());

        if (_playerReference.PlayerMagicSourceType == SourceType.None)
        {
            _stateMachine.SetState(idleEmisionDeactivated);
        }
        else
        {
            _stateMachine.SetState(idleEmisionActivated);  
        }

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> MagicVisionActivated() => () => _playerReference.PlayerMagicSourceType == SourceType.Red || _playerReference.PlayerMagicSourceType == SourceType.Blue || _playerReference.PlayerMagicSourceType == SourceType.Green || _playerReference.PlayerMagicSourceType == SourceType.Colorless;
        Func<bool> MagicVisionDeactivated() => () => _playerReference.PlayerMagicSourceType == SourceType.None;
        Func<bool> MinValueToIdleActivated() => () => _blackboard.Intensity > _blackboard.MinActivatedIdleValue;
        Func<bool> MinValueToIdleDeactivated() => () => _blackboard.Intensity < _blackboard.MinDeactivatedIdleValue;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }
}
