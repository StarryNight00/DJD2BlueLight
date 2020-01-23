using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sounds : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip steps;

    CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        StepsSound();
    }

    private void StepsSound()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && soundSource.isPlaying == false)
        {
            soundSource.clip = steps;
            soundSource.volume = Random.Range(0.4f, 6f);
            soundSource.pitch = Random.Range(0.7f, 1f);
            soundSource.Play();
        }
    }
}
