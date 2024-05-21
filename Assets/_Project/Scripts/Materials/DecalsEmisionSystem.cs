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

        var decalActivate = new StateDecalActivate(_blackboard);
        var decalDeactivate = new StateDecalDeactivate(_blackboard);
        var decalIdleDeactivated = new StateDecalIdleDeactivated(_blackboard);
        var decalIdleActivated = new StateDecalIdleActivated(_blackboard);

        At(decalIdleActivated, decalDeactivate, MagicVisionDeactivated());
        At(decalIdleDeactivated, decalActivate, MagicVisionActivated());
        At(decalDeactivate, decalActivate, MagicVisionActivated());
        At(decalActivate, decalDeactivate, MagicVisionDeactivated());
        At(decalDeactivate, decalIdleDeactivated, MinValueToIdleDeactivated());
        At(decalActivate, decalIdleActivated, MinValueToIdleActivated());

        if (_playerReference.PlayerMagicSourceType == SourceType.None)
        {
            _stateMachine.SetState(decalIdleDeactivated);
        }
        else
        {
            _stateMachine.SetState(decalIdleActivated);  
        }

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> MagicVisionActivated() => () => _playerReference.PlayerMagicSourceType == SourceType.Red || _playerReference.PlayerMagicSourceType == SourceType.Blue || _playerReference.PlayerMagicSourceType == SourceType.Green || _playerReference.PlayerMagicSourceType == SourceType.Colorless;
        Func<bool> MagicVisionDeactivated() => () => _playerReference.PlayerMagicSourceType == SourceType.None;
        Func<bool> MinValueToIdleActivated() => () => _blackboard.Material.color.a > _blackboard.MinActivatedIdleOpacityValue && _blackboard.Intensity > _blackboard.MinActivatedIdleEmisionValue;
        Func<bool> MinValueToIdleDeactivated() => () => _blackboard.Material.color.a < _blackboard.MinDeactivatedIdleOpacityValue && _blackboard.Intensity < _blackboard.MinDeactivatedIdleEmisionValue;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }
}
