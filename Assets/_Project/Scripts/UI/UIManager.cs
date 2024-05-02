using ProjectBPop.Magic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject magicMode;
    [SerializeField] private GameObject interact;

    private void OnEnable()
    {
        MagicSource.OnEnterTriggerArea += ShowInteract;
        MagicSource.OnExitTriggerArea += HideInteract;
        MagicNode.OnEnterTriggerArea += ShowInteract;
        MagicNode.OnExitTriggerArea += HideInteract;
    }

    private void OnDisable()
    {
        MagicSource.OnEnterTriggerArea -= ShowInteract;
        MagicSource.OnExitTriggerArea -= HideInteract;
        MagicNode.OnEnterTriggerArea -= ShowInteract;
        MagicNode.OnExitTriggerArea -= HideInteract;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }
    
    public void DisplayMagicMode()
    {
        magicMode.SetActive(true);
    }

    public void HideMagicMode()
    {
        magicMode.SetActive(false);
    }

    public void ShowInteract()
    {
        interact.SetActive(true);
    }

    public void HideInteract()
    {
        interact.SetActive(false);
    }
}
