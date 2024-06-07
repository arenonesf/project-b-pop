using ProjectBPop.Interfaces;
using UnityEngine;

public class VisionActivator : Mechanism
{
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material stoneMaterial;
    private Animator _animator;
    private int _isShowingHand;
    private string _emissionKeyword = "_EMISSION";

    // Start is called before the first frame update
    void Start()
    {
        _isShowingHand = Animator.StringToHash("isShowingHand");
       
        baseMaterial.DisableKeyword(_emissionKeyword);
        stoneMaterial.DisableKeyword(_emissionKeyword);
    }

    public override void Activate()
    {
        Debug.Log("MEACTIVO");
        base.Activate();
        _animator.SetTrigger(_isShowingHand);
        baseMaterial.EnableKeyword(_emissionKeyword);
        stoneMaterial.EnableKeyword(_emissionKeyword);
    }

    public override void Deactivate()
    {
        Debug.Log("MEDESACTIVO");
        base.Deactivate();
        _animator.SetTrigger(_isShowingHand);
        baseMaterial.DisableKeyword(_emissionKeyword);
        stoneMaterial.DisableKeyword(_emissionKeyword);
    }
}
