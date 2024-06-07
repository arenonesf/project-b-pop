using System;
using ProjectBPop.Interfaces;
using UnityEngine;

public class PerspectivePlatform : Mechanism
{
    private PlayerInteract _playerInteract;

    private void OnEnable()
    {
        GameManager.OnPlayerSet += GetPlayer;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerSet -= GetPlayer;
    }

    public override void Activate()
    {
        base.Activate();
        Solved = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Solved = false;
    }
    
    private void GetPlayer()
    {
        _playerInteract = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
    }
}
