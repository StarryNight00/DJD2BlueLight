using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool IsPaused;

    public GameObject Pause;

    public AudioSource soundSource;
    public AudioClip ding;
    public AudioClip dong;

    public void ExitToMainMenu()
    {
        soundSource.clip = dong;
        soundSource.Play();
        SceneManager.LoadScene(0);
    }

    public void ExitToDesktop()
    {
        soundSource.clip = ding;
        soundSource.Play();
        Application.Quit();
    }

    private void Update()
    {
        if (IsPaused)
        {
            Pause.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Pause.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
    }
}
