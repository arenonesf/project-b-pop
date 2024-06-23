using FMOD;
using FMODUnity;
using ProjectBPop.Input;
using ProjectBPop.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject tutorialImage;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private EventReference enterTutorialEvent;
    private bool _shown;
    private bool _showingTutorial;
    private CharacterController _characterController;
    private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        inputReader.PlayerMagicInteractionEvent += CheckClickToHide;
    }

    private void OnDisable()
    {
        inputReader.PlayerMagicInteractionEvent -= CheckClickToHide;
    }

    private void Awake()
    {
        _shown = false;
        tutorialImage.SetActive(false);
    }

    private void Start()
    {
        _characterController = GameManager.Instance.GetPlayer().GetComponent<CharacterController>();
        _playerMovement = GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_shown) return;
        ShowTutorial();
    }

    private void ShowTutorial()
    {
        _shown = true;
        _showingTutorial = true;
        tutorialImage.SetActive(true);
        _playerMovement.CanMove = false;
        FMOD.Studio.EventInstance enterTutorialInstance = RuntimeManager.CreateInstance(enterTutorialEvent);
        enterTutorialInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        enterTutorialInstance.start();
        enterTutorialInstance.release();
    }

    private void HideTutorial()
    {
        _showingTutorial = false;
        tutorialImage.SetActive(false);
    }

    private void CheckClickToHide()
    {
        if (_showingTutorial)
        {
            HideTutorial();
            _playerMovement.CanMove = true;
        }
    }  
}
