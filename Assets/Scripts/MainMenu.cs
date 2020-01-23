using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Serialized private AudioSource variable
    [SerializeField] private AudioSource soundSource;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip ding;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip dong;

    /// <summary>
    /// Responsible for loading the game scene.
    /// </summary>
    public void PlayGame()
    {

        // Assigns _dong AudioClip to the _soundSource.clip
        // variable
        soundSource.clip = dong;
        // plays _soundSource's current clip
        soundSource.Play();
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
        soundSource.clip = ding;
        // plays _soundSource's current clip
        soundSource.Play();
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
        soundSource.clip = dong;
        // plays _soundSource's current clip
        soundSource.Play();
    }
}
