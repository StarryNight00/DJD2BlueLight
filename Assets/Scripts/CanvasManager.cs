using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing the canvas being displayed on-screen.
/// </summary>
public class CanvasManager : MonoBehaviour
{
    // Serialized private crossHairNormal variable of GameObject type
    [SerializeField] private GameObject _crossHairNormal;
    // Serialized private crossHairInterqact variable of GameObject type
    [SerializeField] private GameObject _crossHairInteract;
    // Serialized private interactionPanel variable of GameObject type
    [SerializeField] private GameObject _interactionPanel;
    // Serialized private choicePanel variable of GameObject type
    [SerializeField] private GameObject _choicePanel;
    // Serialized private nextButton variable of GameObject type
    [SerializeField] private GameObject _nextButton;
    // Serialized private interactionText variable of GameObject type
    [SerializeField] private Text       _interactionText;

    /// <summary>
    /// Responsible for displaying the interaction panel with a specific
    /// message, as well as activating or deactivating the correct parts of the
    /// crosshair.
    /// </summary>
    /// <param name="interactionMessage">Message to be displayed</param>
    public void ShowInteractionPanel(string interactionMessage)
    {
        _interactionText.text = interactionMessage;
        _interactionPanel.SetActive(true);
        _crossHairNormal.SetActive(false);
        _crossHairInteract.SetActive(true);
    }

    /// <summary>
    /// Responsible for hiding the interaction panel and activating and 
    /// deactivating the correct parts of the crosshair.
    /// </summary>
    public void HideInteractionPanel()
    {
        _interactionPanel.SetActive(false);
        _crossHairInteract.SetActive(false);
        _crossHairNormal.SetActive(true);
    }

    /// <summary>
    /// Responsible for activating/displaying the choice panel in the UI.
    /// </summary>
    public void ShowChoicePanel()
    {
        _choicePanel.SetActive(true);
    }

    /// <summary>
    /// Responsible for deactivating/hiding the choice panel in the UI.
    /// </summary>
    public void HideChoicePanel()
    {
        _choicePanel.SetActive(false);
    }

    /// <summary>
    /// Responsible for activating/displaying the button for the next line
    /// in the UI.
    /// </summary>
    public void ShowNextButton()
    {
        _nextButton.SetActive(true);
    }

    /// <summary>
    /// Responsible for deactivating/hiding the button for the next line
    /// in the UI.
    /// </summary>
    public void HideNextButton()
    {
        _nextButton.SetActive(false);
    }

    /// <summary>
    /// Responsible for setting the cursor visibility as needed, using the 
    /// parameter given to determine wether it is visible or not.
    /// </summary>
    /// <param name="visibility">Boolean that determines the cursor's
    /// visibility</param>
    public void SetCursorVisibility(bool visibility)
    {
        Cursor.visible = visibility;
    }

    /// <summary>
    /// Responsible for initializing the necessary variables on start of
    /// running time.
    /// </summary>
    public void Start()
    {
        HideInteractionPanel();
        HideChoicePanel();
        HideNextButton();
    }
}