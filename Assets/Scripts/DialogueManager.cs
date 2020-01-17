using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private Canvas _canvas;
    private CanvasManager _canvasManager;
    private Queue<string> _lines;
    private Text _text;

    // Start is called before the first frame update
    private void Start()
    {
        _lines = new Queue<string>();
        _canvas = FindObjectOfType<Canvas>();
        _canvasManager = _canvas.GetComponent<CanvasManager>();
    }

    public void ShowDialogue(string line)
    {
        _canvasManager.ShowInteractionPanel(line);
    }

    public void InitiateDialogue(Dialogue dialogue)
    {
        for (int i = 0; i < dialogue.Speech.Length; i++)
        {
            _lines.Enqueue(dialogue.Speech[i]);
        }

        for(int i = 0; i < _lines.Count; i++)
        {
            ShowDialogue(_lines.Dequeue());
            // loop while primary mouse button is not pressed, which then
            // updates the dialogue line
            while (!Input.GetMouseButtonDown(0)) { }
        }
    }
}
