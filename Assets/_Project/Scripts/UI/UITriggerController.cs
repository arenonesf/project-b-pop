using UnityEngine;
using UnityEngine.UI;

public class UITriggerController : MonoBehaviour
{
    [SerializeField] private UITriggerInteract dotInteractionTrigger;
    [SerializeField] private UITriggerInteract handInteractionTrigger;
    [SerializeField] private GameObject handUIElement;
    [SerializeField] private GameObject dotUIElement;
    [SerializeField] private Animation fadeOutHand;
    
    private void OnEnable()
    {
        dotInteractionTrigger.OnShowingInteraction += ShowDotInteraction;
        dotInteractionTrigger.OnHidingInteraction += HideDotInteraction;
        handInteractionTrigger.OnShowingInteraction += ShowHandInteraction;
        handInteractionTrigger.OnHidingInteraction += HideHandInteraction;
    }

    private void OnDisable()
    {
        dotInteractionTrigger.OnShowingInteraction -= ShowDotInteraction;
        dotInteractionTrigger.OnHidingInteraction -= HideDotInteraction;
        handInteractionTrigger.OnShowingInteraction -= ShowHandInteraction;
        handInteractionTrigger.OnHidingInteraction -= HideHandInteraction;
    }

    private void ShowDotInteraction(GameObject dot)
    {
        dot.SetActive(true);
    }

    private void HideDotInteraction(GameObject dot)
    {
        dot.SetActive(false);
    }

    private void ShowHandInteraction(GameObject hand)
    {
        var image = hand.GetComponent<Image>();
        var color = image.color;
        color.a = 1f;
        image.color = color;
        hand.SetActive(true);
        dotUIElement.SetActive(false);
    }

    private void HideHandInteraction(GameObject hand)
    {
        //fadeOutHand.Play();
        hand.SetActive(false);
        dotUIElement.SetActive(true);
    }

}
