using ProjectBPop.Interfaces;
using UnityEngine;

public class VisionActivator : Mechanism
{
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material stoneMaterial;
    private Animator _animator;
    private int _isShowingHand;
    private readonly string _emissionKeyword = "_EMISSION";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        _isShowingHand = Animator.StringToHash("showStone");
       
        baseMaterial.DisableKeyword(_emissionKeyword);
        stoneMaterial.DisableKeyword(_emissionKeyword);
    }

    public override void Activate()
    {
        base.Activate();
        _animator.SetTrigger(_isShowingHand);
        baseMaterial.EnableKeyword(_emissionKeyword);
        stoneMaterial.EnableKeyword(_emissionKeyword);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _animator.SetTrigger(_isShowingHand);
        baseMaterial.DisableKeyword(_emissionKeyword);
        stoneMaterial.DisableKeyword(_emissionKeyword);
    }
}
