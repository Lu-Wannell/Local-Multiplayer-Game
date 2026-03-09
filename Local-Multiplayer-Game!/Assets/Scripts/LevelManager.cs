using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject optionsBackground;
    public GameObject creditsBackground;
    public GameObject contolsBackground;

    public void Play()
    {
        SceneManager.LoadScene("Customisation");
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Options()
    {
        optionsBackground.SetActive(true);
    }

    public void OptionsBack()
    {
        optionsBackground.SetActive(false);
    }

    public void Credits()
    {
        creditsBackground.SetActive(true);
    }

    public void CreditsBack()
    {
        creditsBackground.SetActive(false);
    }

    public void Controls()
    {
        contolsBackground.SetActive(true);
    }

    public void ControlsBack()
    {
        contolsBackground.SetActive(false);
    }
}
