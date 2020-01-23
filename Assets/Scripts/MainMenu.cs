using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Serialized private AudioSource variable
    [SerializeField] private AudioSource _soundSource;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip _ding;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip _dong;

    /// <summary>
    /// Responsible for loading the game scene.
    /// </summary>
    public void PlayGame()
    {

        // Assigns _dong AudioClip to the _soundSource.clip
        // variable
        _soundSource.clip = _dong;
        // plays _soundSource's current clip
        _soundSource.Play();
        // load game scene
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Responsible for quitting the game.
    /// </summary>
    public void QuitGame()
    {
        // Assigns _ding AudioClip to the _soundSource.clip
        // variable
        _soundSource.clip = _ding;
        // plays _soundSource's current clip
        _soundSource.Play();
        // quit the application
        Application.Quit();
    }

    /// <summary>
    /// Responsible for playing sound when logo is clicked.
    /// </summary>
    public void Logo()
    {
        // Assigns _dong AudioClip to the _soundSource.clip
        // variable
        _soundSource.clip = _dong;
        // plays _soundSource's current clip
        _soundSource.Play();
    }
}
