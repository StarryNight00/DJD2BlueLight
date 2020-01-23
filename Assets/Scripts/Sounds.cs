using UnityEngine;

/// <summary>
/// Class responsible playing footsteps and door sounds.
/// </summary>
public class Sounds : MonoBehaviour
{
    // Serialized private variable of AudioSource type
    [SerializeField] private AudioSource _soundSource;
    // Serialized private variable of AudioClip type
    [SerializeField] private AudioClip _steps;
    // Serialized private variable of AudioClip type
    [SerializeField] private AudioClip _door;

    // Private variable of type CharacterController
    private CharacterController _cc;

    /// <summary>
    /// Responsible for initializing the necessary variables on start of
    /// running time.
    /// </summary>
    private void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Responsible for updating the state of the object during runtime.
    /// </summary>
    private void Update()
    {
        StepsSound();
    }

    /// <summary>
    /// Responsible for playing footstep sounds while the player is walking
    /// </summary>
    private void StepsSound()
    {
        // if conditions are met, footstep sounds are played
        if (_cc.isGrounded == true && _cc.velocity.magnitude > 2f && 
            _soundSource.isPlaying == false)
        {
            // Assigns _steps AudioClip to the _soundSource.clip
            // variable
            _soundSource.clip = _steps;
            // Assigns random range to volume between 0.4f and 6f
            _soundSource.volume = Random.Range(0.4f, 6f);
            // Assigns random range to pitch between 0.7f and 1f
            _soundSource.pitch = Random.Range(0.7f, 1f);
            // plays _soundSource's current clip
            _soundSource.Play();
        }
    }

    /// <summary>
    /// Responsible for playing sound when the door opens
    /// </summary>
    public void DoorSound()
    {
        // Assigns _door AudioClip to the _soundSource.clip
        // variable
        _soundSource.clip = _door;
        // plays _soundSource's current clip
        _soundSource.Play();
    }
}
