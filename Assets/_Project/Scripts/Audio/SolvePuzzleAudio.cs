using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SolvePuzzleAudio : MonoBehaviour
{
    [SerializeField] private EventReference puzzleCompleteEvent;
    private PuzzleSolver _puzzleSolver;

    private void OnEnable()
    {
        _puzzleSolver.OnPuzzleComplete += PuzzleCompleteSound;
    }

    private void OnDisable()
    {
        _puzzleSolver.OnPuzzleComplete -= PuzzleCompleteSound;
    }

    private void Awake()
    {
        _puzzleSolver = GetComponent<PuzzleSolver>();
    }

    private void PuzzleCompleteSound()
    {
        FMOD.Studio.EventInstance puzzleCompleteInstance = RuntimeManager.CreateInstance(puzzleCompleteEvent);
        puzzleCompleteInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        puzzleCompleteInstance.start();
        puzzleCompleteInstance.release();
    }
}
