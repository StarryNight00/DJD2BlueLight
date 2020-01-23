using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    public Dialogue Dialogue => _dialogue;

    private void OnApplicationQuit()
    {
        Dialogue.ResetDialogue();
    }
}
