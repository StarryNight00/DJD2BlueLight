using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Array of dialogue strings to be fed to a DialogueManager
    [TextArea(1, 5)]
    public string[] lines;
}
