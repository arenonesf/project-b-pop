using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(SendMessage);
    }
    
    private void Start()
    {
        Screen.fullScreen = true;
        toggle.isOn = Screen.fullScreen;
    }

    private static void SendMessage(bool toggleValue)
    {
        Screen.fullScreen = toggleValue;
    }
}
