using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Canvas _canvas;
    private Queue<string> _lines;
    private Text _text;

    // Start is called before the first frame update
    private void Start()
    {
        _lines = new Queue<string>();

    }

    public void ShowDialogue(Text text)
    {

    }

    public void InitiateDialogue(Dialogue dialogue)
    {
       for(int i = 0; i < dialogue.lines.Length; i++)
        {
            _lines.Enqueue(dialogue.lines[i]);
        }


    }
}
