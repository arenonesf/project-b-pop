using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioVolumeManager : MonoBehaviour
{
    [Range(0, 1)]
    public float MasterVolume = 1;
    [Range(0, 1)]
    public float AmbienceVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;
    [Range(0, 1)]
    public float MusicVolume = 1;

    private Bus _masterBus;
    private Bus _ambienceBus;
    private Bus _sfxBus;
    private Bus _musicBus;

    public static AudioVolumeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;

        _masterBus = RuntimeManager.GetBus("bus:/");
        _ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        _sfxBus = RuntimeManager.GetBus("bus:/SFX");
        _musicBus = RuntimeManager.GetBus("bus:/Music");
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        _masterBus.setVolume(MasterVolume);
        _ambienceBus.setVolume(AmbienceVolume);
        _sfxBus.setVolume(SFXVolume);
        _musicBus.setVolume(MusicVolume);
    }
}
