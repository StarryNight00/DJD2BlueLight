using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject interactionPanel;
    public Text interactionText;

    public void ShowInteractionPanel(string interactionMessage)
    {
        interactionText.text = interactionMessage;
        interactionPanel.SetActive(true);
    }
    public void HideInteractionPanel()
    {
        interactionPanel.SetActive(false);
    }

    public void Start()
    {

        HideInteractionPanel();

        //ShowInteractionPanel("dabadiiii");
    }
}
