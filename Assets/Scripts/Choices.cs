using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    public GameObject TextBox;
    public GameObject TextBoxBackground;
    public GameObject Choice01;
    public GameObject Choice02;
    public GameObject Choice03;
    public GameObject Choice04;
    public int ChoiceMade;

    public void ChoiceButton01()
    {
        TextBox.GetComponent<Text>().text = "Currently working on it :D";
        ChoiceMade = 1;
    }
    public void ChoiceButton02()
    {
        TextBox.GetComponent<Text>().text = "Why don't eggs tell jokes? They'd crack each other up";
        ChoiceMade = 2;
    }
    public void ChoiceButton03()
    {
        TextBox.GetComponent<Text>().text = "A flock of crows in known as a murder";
        ChoiceMade = 3;
    }
    public void ChoiceButton04()
    {
        TextBox.GetComponent<Text>().text = "Why don't eggs tell jokes? They'd crack each other up";
        ChoiceMade = 4;
    }

    public void Update()
    {
        if (ChoiceMade == 4)
        {
            Choice01.SetActive(false);
            Choice02.SetActive(false);
            Choice03.SetActive(false);
            Choice04.SetActive(false);
            TextBox.SetActive(false);
            TextBoxBackground.SetActive(false);
        }
    }
}
