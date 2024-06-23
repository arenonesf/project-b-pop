using FMOD;
using ProjectBPop.Input;
using ProjectBPop.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject tutorialImage;
    [SerializeField] private InputReader inputReader;
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
        _characterController.enabled = false;
        _playerMovement.enabled = false;
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
            _characterController.enabled = true;
            _playerMovement.enabled = true;
            _characterController.Move(Vector3.zero);
        }
    }  
}
