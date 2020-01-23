using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject responsible for holding all the information of a Dialogue,
/// to be used by the dialogue system to engage the player in conversations.
/// </summary>
[CreateAssetMenu(menuName = "Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{
    [Header("Collections of Dialogue and Choice lines")]
    // Array that holds the full lines of dialogue
    [TextArea]
    [SerializeField] private string[] _speech = null;
    // Array of integers that holds the lines at which the player has a choice
    [SerializeField] private int[] _choiceLines;
    [Header("Current information variables")]
    // Integer that holds the current line of dialogue
    [SerializeField] private int _currentLine = 0;
    // Integer that holds the currentChoice in the array _choiceLines
    [SerializeField] private int _currentChoice = default;

    // Public property to grant access to the private speech string array
    public string[] Speech => _speech;
    // public property to grant access to the private choiceLines string array 
    public int[] ChoiceLines => _choiceLines;
    // public property to grant access to the private int currentLine
    public int CurrentLine => _currentLine;
    // public property to grant access to the private int choice
    public int CurrentChoice => _currentChoice;

    /// <summary>
    /// Public method used to update the current line of dialogue that is
    /// being read, keeping in mind a choice made by the player.
    /// Used to determine what line of dialogue is shown after the player
    /// makes a choice.
    /// </summary>
    public void UpdateDialogueWithChoice(int choice)
    {
        _currentLine = Mathf.Min(_speech.Length - 1, _currentLine + choice);
    }

    /// <summary>
    /// Public method used to update the current line of dialogue that is being
    /// read, incrementing it by 1.
    /// Used to update normal dialogue.
    /// </summary>
    public void IncrementDialogueLine()
    {
        if (_currentLine < _speech.Length - 4) _currentLine++;
        Debug.Log($"Current Line: {_currentLine}");
    }

    /// <summary>
    /// Responsible for resetting the Dialogue scriptable object.
    /// (Usable only while editor, as ScriptableObjects' information is not
    /// reset after running the project in the editor).
    /// </summary>
    public void ResetDialogue()
    {
        _currentLine = 0;
        _currentChoice = 0;
    }
}
