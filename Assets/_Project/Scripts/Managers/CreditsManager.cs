using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    private ScreenFader _fader;

    private void Start()
    {
        _fader = FindObjectOfType<ScreenFader>();
        _fader.OnFadeInComplete += LoadMainMenu;
        _fader.FadeOutImage();
    }

    private void OnDestroy()
    {
        _fader.OnFadeInComplete -= LoadMainMenu;
    }

    public void EndCreditsAnimationEvent()
    {
        _fader.gameObject.SetActive(true);
        _fader.FadeInImage();
    }

    private void LoadMainMenu()
    {
        inputReader.SetUI();
        SceneManager.LoadScene(0);
    }
}
