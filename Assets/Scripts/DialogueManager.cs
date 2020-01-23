using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Class responsible for managing the dialogue displayed on-screen.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    // private _canvas variable of Canvas type
    private Canvas _canvas;
    // private _canvasManager variable of CanvasManager type
    private CanvasManager _canvasManager;

    /// <summary>
    /// Responsible for initializing the necessary variables on start of
    /// running time.
    /// </summary>
    private void Start()
    {
        _canvas = FindObjectOfType<Canvas>();
        _canvasManager = _canvas.GetComponent<CanvasManager>();
    }

    /// <summary>
    /// Responsible for displaying single lines of dialogue in the canvas.
    /// </summary>
    /// <param name="line">Line to be displayed</param>
    public void ShowDialogue(string line)
    {
        _canvasManager.ShowInteractionPanel(line);
    }
}
