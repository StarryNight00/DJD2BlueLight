using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{
    // Array that holds the full lines of dialogue
    [SerializeField] private string[] _speech = null;
    // Integer that holds the current line of dialogue
    [SerializeField] private int _currentLine = 0;
    // Integer that holds the player's choice ID, used to navigate the speech
    // bidimensional array in order to display the correct dialogue on-screen
    [SerializeField] private int _choice = default;
    // Dictionary that holds the available dialogue choices, using an integer
    // as the key, which corresponds to the speech's line that the player
    // will be given the choice after, said choices being stored in a string
    // array, the index of the answer selected being later used to determine
    // the choice variable value
    [SerializeField] private Dictionary<int, string[]> choiceLines = null;

    // Public property to grant access to the speech bidimensional string array
    public string[] Speech => _speech;
    // public property to grant access to the private int currentLine
    public int CurrentLine => _currentLine;
    // public property to grant access to the private int choice
    public int Choice => _choice;
    // public property to grant access to the private Dictionary choiceLines
    public Dictionary<int, string[]> ChoiceLines => choiceLines;

    /// <summary>
    /// Public method used to update the current line of dialogue that is
    /// being read, keeping in mind a choice made by the player.
    /// Used to determine what line of dialogue is shown after the player
    /// makes a choice.
    /// </summary>
    public void UpdateDialogueLineWithChoice()
    {
        _currentLine += _choice;
    }

    /// <summary>
    /// Public method used to update the current line of dialogue that is being
    /// read, incrementing it by 1.
    /// Used to update normal dialogue.
    /// </summary>
    public void IncrementDialogueLine()
    {
        if (_currentLine < _speech.Length - 1) _currentLine++;
        else if (_currentLine == _speech.Length - 1) _currentLine = 0;
    }
}
