using System;
using ProjectBPop.Magic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITriggerController : MonoBehaviour
{
    [SerializeField] private UITriggerInteract dotInteractionTrigger;
    [SerializeField] private UITriggerInteract handInteractionTrigger;
    [SerializeField] private GameObject handUIElement;
    [SerializeField] private GameObject dotUIElement;
    [SerializeField] private Animation fadeOutHand;

    private MagicArtifact _artifact;
    private PlayerInteract _playerInteract;

    private void Awake()
    {
        _artifact = GetComponentInParent<MagicArtifact>();
    }

    private void OnEnable()
    {
        dotInteractionTrigger.OnShowingInteraction += ShowDotInteraction;
        dotInteractionTrigger.OnHidingInteraction += HideDotInteraction;
        handInteractionTrigger.OnShowingInteraction += ShowHandInteraction;
        handInteractionTrigger.OnHidingInteraction += HideHandInteraction;
        GameManager.OnPlayerSet += FindPlayer;
    }

    private void OnDisable()
    {
        dotInteractionTrigger.OnShowingInteraction -= ShowDotInteraction;
        dotInteractionTrigger.OnHidingInteraction -= HideDotInteraction;
        handInteractionTrigger.OnShowingInteraction -= ShowHandInteraction;
        handInteractionTrigger.OnHidingInteraction -= HideHandInteraction;
        GameManager.OnPlayerSet -= FindPlayer;
    }

    private void ShowDotInteraction(GameObject dot)
    {
        if (!_artifact.Active && _playerInteract.PlayerMagicSourceType != _artifact.ArtifactSourceType)
        {
            return;
        }
        
        if (_artifact.Active && _playerInteract.PlayerMagicSourceType != SourceType.None)
        {
            return;
        }
        
        dot.SetActive(true);
    }

    private void HideDotInteraction(GameObject dot)
    {
        if (!_artifact.Active && _playerInteract.PlayerMagicSourceType != _artifact.ArtifactSourceType)
        {
            return;
        }
        
        dot.SetActive(false);
    }

    private void ShowHandInteraction(GameObject hand)
    {
        if (_artifact.Active && _playerInteract.PlayerMagicSourceType != SourceType.None)
        {
            return;
        }

        if (!_artifact.Active && _playerInteract.PlayerMagicSourceType != _artifact.ArtifactSourceType)
        {
            return;
        }
        
        var image = hand.GetComponent<Image>();
        var color = image.color;
        color.a = 1f;
        image.color = color;

        hand.SetActive(true);
        dotUIElement.SetActive(false);
    }

    private void HideHandInteraction(GameObject hand)
    {
        if (_artifact.Active && _playerInteract.PlayerMagicSourceType != SourceType.None)
        {
            return;
        }

        if (!_artifact.Active && _playerInteract.PlayerMagicSourceType != _artifact.ArtifactSourceType)
        {
            return;
        }
        hand.SetActive(false);
        dotUIElement.SetActive(true);
    }
    
    private void FindPlayer()
    {
        _playerInteract = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
    }
}
