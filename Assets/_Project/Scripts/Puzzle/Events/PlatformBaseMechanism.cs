using ProjectBPop.Interfaces;
using UnityEngine;

public class PlatformBaseMechanism : Mechanism
{
    [SerializeField] private GameObject particles;

    public override void Activate()
    {
        base.Activate();
        particles.SetActive(true);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        particles.SetActive(false);
    }
}
