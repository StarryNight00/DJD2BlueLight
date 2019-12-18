using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject crossHairNormal;
    public GameObject crossHairInteract;
    public GameObject interactionPanel;
    public Text interactionText;

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

    public void Start()
    {

        HideInteractionPanel();

        //ShowInteractionPanel("dabadiiii");
    }
}