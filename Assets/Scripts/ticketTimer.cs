using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicketTimer : MonoBehaviour
{
    //RELATED TO SOUND
    //MAKE OWN CLASS

    public AudioSource  soundSource;
    public AudioClip    ding;
    public AudioClip    dong;
    public Animator     animator;
    public GameObject   door;

    //private float  currentTime;

    public TextMeshProUGUI  timeCount;
    public bool             canEnter;

    private float           timeWait;
    private int             currentTicket;
    private float           ticket;
    private float           fixedDeltaTimeUnit;
    private bool            isWaiting;


    private void Start()
    {
        animator = door.GetComponent<Animator>();
        currentTicket = Random.Range(1, 6);
        timeWait = 0;
        ticket = 30;
        canEnter = false;

        isWaiting = false;

        fixedDeltaTimeUnit = Time.fixedUnscaledDeltaTime;
    }
    private void Update()
    {
        NPCTimeWait();
        CheckIfDone();
    }

    private void FixedUpdate()
    {
        UpdateTimeDisplay();
    }

    private void NPCTimeWait()
    {
        int seconds = Mathf.FloorToInt(60 * fixedDeltaTimeUnit);
        int oneMinute = seconds * 60;
        int threeMinutes = oneMinute * 3;

        if (isWaiting == false)
        {
            timeWait = Random.Range(oneMinute, threeMinutes); //1 to 3 minutes
            timeWait = timeWait * 60;
            isWaiting = true;
        }
        else
        {
            timeWait -= seconds;
            
            if (Input.GetKeyDown("u"))
            {
                currentTicket = 420;
                canEnter = true;
            }
            else if (Input.GetKeyDown("y"))
            {
                currentTicket += 1;
                soundSource.clip = dong;
                soundSource.Play();
                isWaiting = false;
            }

            if (timeWait <= 0)
            {
                isWaiting = false;
                currentTicket += 1;
                if(currentTicket < 420)
                {
                    soundSource.clip = dong;
                    soundSource.Play();
                }
                if (currentTicket == 420)
                {
                    soundSource.clip = ding;
                    soundSource.Play();
                    canEnter = true;
                }
            }
        }
    }

    private void UpdateTimeDisplay()
    {
        if (currentTicket < 10)
        {
            timeCount.text = "00" + currentTicket;
        }
        else if (currentTicket < 100)
        {
            timeCount.text = "0" + currentTicket;
        }
        else if (currentTicket < 1000)
        {
            timeCount.text = "" + currentTicket;
        }
    }


/////////////////---- ATTENTION
    private void CheckIfDone()
    {
        if(currentTicket >= ticket)
        {
            animator.SetTrigger("trigger");
        }
    }
}
