using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling the pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    private bool _isPaused;
    // Serialized private GameObject, used to contain the pause panel
    [SerializeField] private GameObject Pause;
    // Serialized private AudioSource variable
    [SerializeField] private AudioSource soundSource;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip ding;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip dong;

    /// <summary>
    /// Responsible for exiting to the Main Menu scene.
    /// </summary>
    public void ExitToMainMenu()
    {
        // Assign _dong AudioClip to the _soundSource.clip
        // variable
        soundSource.clip = dong;
        // play _soundSource's current clip
        soundSource.Play();
        // load Main Menu scene
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Responsible for exiting from the game to the desktop.
    /// </summary>
    public void ExitToDesktop()
    {
        // Assign _ding AudioClip to the _soundSource.clip
        // variable
        soundSource.clip = ding;
        // play _soundSource's current clip
        soundSource.Play();
        // quit application
        Application.Quit();
    }

    /// <summary>
    /// Responsible for updating the state of the object during runtime.
    /// </summary>
    private void Update()
    {
        // if statement that checks if _isPaused bool is true
        if (_isPaused)
        {
            // set Pause object's active state to true
            Pause.SetActive(true);
            // assign 0f to timeScale, so the player cant move while on pause
            Time.timeScale = 0f;
        }
        else
        {
            // set Pause object's active state to false
            Pause.SetActive(false);
            // assign 1f to timeScale, so the player can move again
            Time.timeScale = 1f;
        }
        // if Escape key is pressed set _isPaused bool to it's contrary
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;
        }
    }
}
