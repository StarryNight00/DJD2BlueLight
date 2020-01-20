using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    public Dialogue Dialogue => _dialogue;

    private void OnMouseDown()
    {
        Dialogue.IncrementDialogueLine();
        Debug.Log("Dialogue line incremented.");
    }

    private void OnApplicationQuit()
    {
        Dialogue.ResetDialogueLine();
    }
}
