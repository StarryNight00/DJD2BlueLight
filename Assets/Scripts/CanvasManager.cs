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
    public Image[] inventoryIcons;

    public void Start()
    {
        //ShowInteractionPanel("I am, in fact, working as expected");
        HideInteractionPanel();
    }

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

    public void SetInventoryIcon(int i, Sprite icon)
    {
        inventoryIcons[i].sprite = icon;
        inventoryIcons[i].color = Color.white;
    }

    public void ClearInventoryIcons()
    {
        for (int i = 0; i < inventoryIcons.Length; ++i)
        {
            inventoryIcons[i].sprite = null;
            inventoryIcons[i].color = Color.clear;
        }
    }
}
