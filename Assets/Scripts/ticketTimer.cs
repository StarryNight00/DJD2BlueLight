using UnityEngine;
using TMPro;

/// <summary>
/// Class responsible for the ticket timer displayed in the waiting room.
/// </summary>
public class TicketTimer : MonoBehaviour
{
    // Serialized private AudioSource variable
    [SerializeField] private AudioSource  _soundSource;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip    _ding;
    // Serialized private AudioClip variable
    [SerializeField] private AudioClip    _dong;
    // Serialized private Animator variable
    [SerializeField] private Animator _animator;
    // Serialized private GameObject variable
    [SerializeField] private GameObject _door;

    // Serialized private TextmeshProGUI variable
    [SerializeField] private TextMeshProUGUI _timeCount;
    // Serialized private bool variable
    [SerializeField] private bool _canEnter;

    // private timeWait variable of float type
    private float           _timeWait;
    // private currentTicket variable of int type
    private int             _currentTicket;
    // private ticket variable of float type
    private float           _ticket;
    // private fixedDeltaTimeUnit variable of float type
    private float           _fixedDeltaTimeUnit;
    // private isWaiting variable of bool type
    private bool            _isWaiting;

    /// <summary>
    /// Responsible for initializing the necessary variables on start of
    /// running time.
    /// </summary>
    private void Start()
    {
        _animator = _door.GetComponent<Animator>();
        _currentTicket = Random.Range(1, 6);
        _timeWait = 0;
        _ticket = 420;
        _canEnter = false;

        _isWaiting = false;

        _fixedDeltaTimeUnit = Time.fixedUnscaledDeltaTime;
    }

    /// <summary>
    /// Responsible for updating the state of the object during runtime.
    /// </summary>
    private void Update()
    {
        NPCTimeWait();
        CheckIfDone();
    }

    /// <summary>
    /// Responsible for updating the state of the object during runtime in
    /// tandem with real time.
    /// </summary>
    private void FixedUpdate()
    {
        UpdateTimeDisplay();
    }

    /// <summary>
    /// Sets the wait time until ticket number passes and functionality of
    /// cheats.
    /// </summary>
    private void NPCTimeWait()
    {
        // Variable of type int that determines the time that passed in seconds
        int seconds = Mathf.FloorToInt(60 * _fixedDeltaTimeUnit);
        // Variable of type int that determines the time in minutes
        int oneMinute = seconds * 60;
        // Variable of type int that converts oneMinutes into three minutes
        int threeMinutes = oneMinute * 3;

        // if statement that determines a random time until the next ticket
        // between 1 and 3 minutes if _isWaiting bool is false or removes
        // time passed in seconds from _timeWait
        if (_isWaiting == false)
        {
            // Determines _timeWait from random range between oneMinute and
            // threeMinutes variables' values
            _timeWait = Random.Range(oneMinute, threeMinutes);
            _timeWait = _timeWait * 60;
            // sets _isWaiting bool to true
            _isWaiting = true;
        }
        else
        {
            _timeWait -= seconds;
            // Cheat to pass all tickets
            if (Input.GetKeyDown("u"))
            {
                // Sets _currentTicket variable to 420
                _currentTicket = 420;
                // plays _soundSource's current clip
                _soundSource.clip = _ding;
                // plays _soundSource's current clip
                _soundSource.Play();
                // sets _isWaiting bool to false
                _canEnter = true;
            }
            // Cheat to pass one ticket
            else if (Input.GetKeyDown("y"))
            {
                // Increments _currentTicket variable
                _currentTicket += 1;
                // Assigns _dong AudioClip to the _soundSource.clip
                // variable
                _soundSource.clip = _dong;
                // plays _soundSource's current clip
                _soundSource.Play();
                // sets _isWaiting bool to false
                _isWaiting = false;
            }
            // if statement that sets _isWaiting to false and increments
            // _currentTicket if _timeWait is 0
            if (_timeWait <= 0)
            {
                _isWaiting = false;
                _currentTicket += 1;
                // if statement that plays _dong AudioClip when _currentTicket
                // is 420
                if(_currentTicket < 420)
                {
                    // Assigns _dong AudioClip to the _soundSource.clip
                    // variable
                    _soundSource.clip = _dong;
                    // plays _soundSource's current clip
                    _soundSource.Play();
                }
                if (_currentTicket == 420)
                {
                    // Assigns _ding AudioClip to the _soundSource.clip
                    // variable
                    _soundSource.clip = _ding;
                    // plays _soundSource's current clip
                    _soundSource.Play();
                    // sets can enter bool to true
                    _canEnter = true;
                }
            }
        }
    }

    private void UpdateTimeDisplay()
    {
        if (_currentTicket < 10)
        {
            _timeCount.text = "00" + _currentTicket;
        }
        else if (_currentTicket < 100)
        {
            _timeCount.text = "0" + _currentTicket;
        }
        else if (_currentTicket < 1000)
        {
            _timeCount.text = "" + _currentTicket;
        }
    }

    private void CheckIfDone()
    {
        if(_currentTicket >= _ticket)
        {
            _animator.SetTrigger("trigger");
        }
    }
}
