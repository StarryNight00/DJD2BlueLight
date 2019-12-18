using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        // Find DialogueManager and call Initiate Dialogue
        FindObjectOfType<DialogueManager>().InitiateDialogue(dialogue);
    }
}
