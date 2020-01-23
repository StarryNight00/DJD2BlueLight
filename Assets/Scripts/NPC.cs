using UnityEngine;

/// <summary>
/// Class responsible for handling NPC dialogue.
/// </summary>
public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    public Dialogue Dialogue => _dialogue;

    private void OnApplicationQuit()
    {
        Dialogue.ResetDialogue();
    }
}
