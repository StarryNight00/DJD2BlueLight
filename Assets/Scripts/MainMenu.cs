using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip ding;
    public AudioClip dong;

    public void PlayGame()
    {
        soundSource.clip = dong;
        soundSource.Play();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        soundSource.clip = ding;
        soundSource.Play();
        Application.Quit();
    }

    public void Logo()
    {
        soundSource.clip = dong;
        soundSource.Play();
    }
}
