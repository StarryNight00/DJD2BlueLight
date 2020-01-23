using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject crossHairNormal;
    [SerializeField] private GameObject crossHairInteract;
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private Text interactionText;


    public void ShowInteractionPanel(string interactionMessage)
    {
        interactionText.text = interactionMessage;
        interactionPanel.SetActive(true);
        crossHairNormal.SetActive(false);
        crossHairInteract.SetActive(true);
    }
    public void HideInteractionPanel()
    {
        interactionPanel.SetActive(false);
        crossHairInteract.SetActive(false);
        crossHairNormal.SetActive(true);
    }

    public void ShowChoicePanel()
    {
        choicePanel.SetActive(true);
    }

    public void HideChoicePanel()
    {
        choicePanel.SetActive(false);
    }

    public void ShowNextButton()
    {
        nextButton.SetActive(true);
    }

    public void HideNextButton()
    {
        nextButton.SetActive(false);
    }

    public void SetCursorVisibility(bool visibility)
    {
        Cursor.visible = visibility;
    }

    public void Start()
    {
        HideInteractionPanel();
        HideChoicePanel();
        HideNextButton();
    }
}