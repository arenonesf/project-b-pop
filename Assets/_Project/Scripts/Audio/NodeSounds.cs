using FMODUnity;
using ProjectBPop.Magic;
using UnityEngine;

public class NodeSounds : MonoBehaviour
{
    private StudioEventEmitter _emitter;
    [SerializeField] private EventReference activateNodeEvent;
    [SerializeField] private EventReference deactivateNodeEvent;
    private MagicNode _magicNode;

    private void OnEnable()
    {
        _magicNode.GiveMagicSingle += ActivateSound;
        _magicNode.TakeMagicParticleSingle += DeactivateSound;
    }

    private void OnDisable()
    {
        _magicNode.GiveMagicSingle -= ActivateSound;
        _magicNode.TakeMagicParticleSingle -= DeactivateSound;
    }

    private void Awake()
    {
        _emitter = GetComponent<StudioEventEmitter>();
        _magicNode = GetComponent<MagicNode>();
    }

    private void Start()
    {
        if (_magicNode.Active)
        {
            _emitter.Play();
        }
        else
        {
            _emitter.Stop();
        }
    }

    private void ActivateSound()
    {
        FMOD.Studio.EventInstance activateNodeInstance = RuntimeManager.CreateInstance(activateNodeEvent);
        activateNodeInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        activateNodeInstance.start();
        activateNodeInstance.release();
        _emitter.Play();
    }

    private void DeactivateSound()
    {
        _emitter.Stop();
        FMOD.Studio.EventInstance deactivateNodeInstance = RuntimeManager.CreateInstance(deactivateNodeEvent);
        deactivateNodeInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        deactivateNodeInstance.start();
        deactivateNodeInstance.release();
    }
}
