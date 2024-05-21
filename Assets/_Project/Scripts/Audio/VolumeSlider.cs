using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        AMBIENCE,
        SFX,
        MUSIC
    }

    [SerializeField] VolumeType volumeType;
    private Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();    
    }

    private void Update()
    {
        UpdateVolumeSlider();
    }
    private void UpdateVolumeSlider()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                _volumeSlider.value = AudioVolumeManager.Instance.MasterVolume;
                break;
            case VolumeType.AMBIENCE:
                _volumeSlider.value = AudioVolumeManager.Instance.AmbienceVolume;
                break;
            case VolumeType.SFX:
                _volumeSlider.value = AudioVolumeManager.Instance.SFXVolume;
                break;
            case VolumeType.MUSIC:
                _volumeSlider.value = AudioVolumeManager.Instance.MusicVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not supported");
                break;
        }
    }

    public void OnSliderValueChange()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioVolumeManager.Instance.MasterVolume = _volumeSlider.value;
                break;
            case VolumeType.AMBIENCE:
                AudioVolumeManager.Instance.AmbienceVolume = _volumeSlider.value;
                break;
            case VolumeType.SFX: 
                AudioVolumeManager.Instance.SFXVolume = _volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                AudioVolumeManager.Instance.MusicVolume = _volumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume Type not supported");
                break;
        }
    } 
}
