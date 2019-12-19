using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ticketTimer : MonoBehaviour
{
    //RELATED TO SOUND
    //MAKE OWN CLASS

    public AudioSource soundSource;
    public AudioClip ding;
    public AudioClip dong;
    public Animator animator;
    public GameObject door;

    //private float  currentTime;

    public Text     timeCount;
    private int     timeWait;
    private int     currentTicket;
    private float   clockOneHour;
    private float   ticket;
    private bool    isWaiting;

    private void Start()
    {
        animator = door.GetComponent<Animator>();
        soundSource.clip = dong;
        clockOneHour = 60.0f;
        currentTicket = Random.Range(1, 6);
        timeWait = 0;
        ticket = 420;
    }
    private void Update()
    {
        UpdateTimeDisplay();
        NPCTimeWait();
        CheckIfDone();
        if(currentTicket == ticket)
        {
            soundSource.clip = ding;
            soundSource.Play();
        }
    }

    private void FixedUpdate()
    {
        if (clockOneHour <= 0)
            clockOneHour = 60;

        clockOneHour -= Time.fixedDeltaTime;
    }

    private void NPCTimeWait()
    {
        if (isWaiting == false)
        {
            timeWait = Random.Range(120, 360); //1 to 3 minutes
            isWaiting = true;
        }
        else
        {
            timeWait -= 1;
            
            if (Input.GetKeyDown("u"))
            {
                currentTicket = 420;
            }

            if (timeWait <= 0)
            {
                isWaiting = false;
                currentTicket += 1;
                if(currentTicket < 420)
                {
                    soundSource.Play();
                }
            }
        }
    }

    private void UpdateTimeDisplay()
    {
        //currentTime = clockOneHour;

        //int time = Mathf.FloorToInt(currentTime);
        //minutesWanted * 60
        //int minutes = time / 60;
        //int seconds = time - (minutes * 60);


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

        /*
        Debug.Log("clockOneHour " + clockOneHour);
        Debug.Log("currentTime " + currentTime);
        Debug.Log("currentTicket " + currentTicket);
        Debug.Log("timeWait " + timeWait);
        */
    }

    private void CheckIfDone()
    {
        if(currentTicket >= ticket)
        {
            animator.SetTrigger("trigger");
        }
    }
}
