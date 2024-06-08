using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePuzzleAudio : MonoBehaviour
{
    [SerializeField] private EventReference perspectiveAligningEvent;
    [SerializeField] private EventReference alignmentCompletedEvent;
    private VisualAlignment _visualAlignment;
    private FMOD.Studio.EventInstance _perspectiveAligningInstance;

    private void OnEnable()
    {
        _visualAlignment.OnAligning += PlayVisualAligning;
        _visualAlignment.OnStopAligning += StopVisualAligning;
        VisualAlignment.OnVisionCompleted += PlayAlignmentComplete;
    }

    private void OnDisable()
    {
        _visualAlignment.OnAligning -= PlayVisualAligning;
        _visualAlignment.OnStopAligning -= StopVisualAligning;
        VisualAlignment.OnVisionCompleted -= PlayAlignmentComplete;
        _perspectiveAligningInstance.release();
    }

    private void Awake()
    {
       _visualAlignment = GetComponent<VisualAlignment>();
        _perspectiveAligningInstance = RuntimeManager.CreateInstance(perspectiveAligningEvent);
       _perspectiveAligningInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    private void PlayVisualAligning()
    {
        _perspectiveAligningInstance.start();
    }

    private void PlayAlignmentComplete()
    {
        _perspectiveAligningInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMOD.Studio.EventInstance alignmentCompletedInstance = RuntimeManager.CreateInstance(alignmentCompletedEvent);
        alignmentCompletedInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        alignmentCompletedInstance.start();
        alignmentCompletedInstance.release();
    }

    private void StopVisualAligning()
    {
        _perspectiveAligningInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
