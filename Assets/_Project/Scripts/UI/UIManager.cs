using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject magicMode;
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
}
