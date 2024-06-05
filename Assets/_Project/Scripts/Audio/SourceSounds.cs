using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ProjectBPop.Magic;

public class SourceSounds : MonoBehaviour
{
    private StudioEventEmitter _emitter;
    private MagicSource _magicSource;

    private void OnEnable()
    {
        _magicSource.GiveMagicSingle += PlayEmitter;
        _magicSource.TakeMagicParticleSingle += StopEmitter;
    }

    private void OnDisable()
    {
        _magicSource.GiveMagicSingle -= PlayEmitter;
        _magicSource.TakeMagicParticleSingle -= StopEmitter;
    }

    private void Awake()
    {
        _emitter = GetComponent<StudioEventEmitter>();
        _magicSource = GetComponent<MagicSource>();
    }

    private void Start()
    {
        if (_magicSource.Active)
        {
            _emitter.Play();
        }
        else
        {
            _emitter.Stop();
        }
    }

    private void PlayEmitter()
    {
        _emitter.Play();
    }

    private void StopEmitter()
    {
        _emitter.Stop();
    }


}
