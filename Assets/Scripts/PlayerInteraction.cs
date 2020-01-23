using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private const float MAX_INTERACTION_DISTANCE = 2.5f;

    private InteractableItem _currentItem;
    private bool _hasRequirements;
    private Transform _cameraTransform;
    private List<InteractableItem> _inventory;
    private Player _player;

    [SerializeField] private CanvasManager _canvasManager;

    // ---------------------------TEST CODE HERE-------------------------------
    private NPC _currentNPC;

    public NPC CurrentNPC => _currentNPC;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _currentItem = null;
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _inventory = new List<InteractableItem>();
        _canvasManager.SetCursorVisibility(false);
    }
    private void Update()
    {
        Debug.Log("Begin update");
        Debug.Log($"Order Karma: {_player.PlayerKarma.OrderKarma} " +
            $"Freedom Karma: {_player.PlayerKarma.FreedomKarma}");
        CheckForInteractive();
        CheckForPlayerInteraction();
    }

    private void CheckForInteractive()
    {
        Debug.Log("Checking for any interactive");
        if (Physics.Raycast(_cameraTransform.position,
                            _cameraTransform.forward,
                            out RaycastHit hitInfo,
                            MAX_INTERACTION_DISTANCE))
        {
            if (hitInfo.collider.TryGetComponent<InteractableItem>(
                out InteractableItem newItem))
            {
                Debug.Log("Found interactive item");
                CheckNewItem(newItem);
            }
            if (hitInfo.collider.TryGetComponent<NPC>(out NPC newNPC))
            {
                Debug.Log("Found interactive NPC");
                CheckNewNPC(newNPC);
            }
        }
        else _canvasManager.HideInteractionPanel();
    }

    private void CheckNewItem(InteractableItem newItem)
    {
        Debug.Log("Start checking of new interactive item");
        if (newItem != null && newItem != _currentItem)
        {
            Debug.Log("New interactive item is valid");
            SetCurrentInteractive(newItem);
        }
        else if (newItem == null)
        {
            Debug.Log("New interactive item is invalid as it is null");
            ClearCurrentInteractive();
        }
        else
        {
            Debug.Log("New interactive item is invalid for unknown reasons");
            ClearCurrentInteractive();
        }
    }

    private void CheckNewNPC(NPC newNPC)
    {
        Debug.Log("Start checking of new interactive NPC");
        if (newNPC != null && newNPC != _currentNPC)
        {
            Debug.Log("New interactive NPC is valid");
            SetCurrentNPC(newNPC);
        }
        else if (newNPC == _currentNPC)
        {
            DisplaySpeech();
        }
        else
        {
            Debug.Log("New interactive NPC is invalid as it is null");
            ClearCurrentNPC();
        }
    }

    private void SetCurrentInteractive(InteractableItem newItem)
    {
        _currentItem = newItem;
        Debug.Log("Current interactive item set, checking requirements");
        if (HasInteractionRequirements())
        {
            Debug.Log("Item interaction requirements met; Interacting");
            _hasRequirements = true;
            _canvasManager.ShowInteractionPanel(_currentItem.interactionText);
        }
        else
        {
            Debug.Log("Item interaction requirements not met; Displaying" +
                "requirements");
            _hasRequirements = false;
            _canvasManager.ShowInteractionPanel(_currentItem.requirementText);
        }
    }

    // -----------------------------TEST CODE HERE-----------------------------
    private void SetCurrentNPC(NPC newNPC)
    {
        if (newNPC != null)
        {
            _currentNPC = newNPC;

            _player.SetInteractionState(true);
            _canvasManager.SetCursorVisibility(true);
            DisplaySpeech();
        }
    }

    private void DisplaySpeech()
    {
        Debug.Log($"Player Karma - Order: {_player.PlayerKarma.OrderKarma}" +
            $"Freedom: {_player.PlayerKarma.FreedomKarma}");
        Debug.Log("Current interactive NPC set, displaying dialogue");
        _canvasManager.ShowInteractionPanel(
            _currentNPC.Dialogue.Speech[_currentNPC.Dialogue.CurrentLine]);
        Debug.Log("Line displayed");

        _canvasManager.ShowNextButton();
        _canvasManager.HideChoicePanel();

        if(_currentNPC.Dialogue.CurrentLine ==
            _currentNPC.Dialogue.ChoiceLines[
                _currentNPC.Dialogue.CurrentChoice])
        {
            _canvasManager.HideNextButton();
            _canvasManager.ShowChoicePanel();
        }

        if (_currentNPC.Dialogue.CurrentLine >=
            _currentNPC.Dialogue.Speech.Length - 3)
        {
            _canvasManager.HideNextButton();
            _player.SetInteractionState(false);
            _canvasManager.SetCursorVisibility(false);
        }
    }

    private bool HasInteractionRequirements()
    {
        if (_currentItem.requirementText == null)
            return true;
        for (int i = 0; i < _currentItem.activationChain.Length; ++i)
            if (!HasInInventory(_currentItem.activationChain[i]))
                return false;

        return true;
    }

    private void ClearCurrentInteractive()
    {
        _currentItem = null;
        //_canvasManager.HideInteractionPanel();
    }

    private void ClearCurrentNPC()
    {
        _currentNPC = null;
        //_canvasManager.HideInteractionPanel();
    }

    private void CheckForPlayerInteraction()
    {
        if (Input.GetMouseButtonDown(0) && _currentItem != null)
        {
            if (_currentItem.type == InteractableItem.InteractiveType.PICKABLE)
                Pick();
            else
                Interact();
        }
    }

    private void Pick()
    {
        AddToInventory(_currentItem);
        _currentItem.gameObject.SetActive(false);
    }

    private void Interact()
    {
        if (_hasRequirements)
        {
            for (int i = 0; i < _currentItem.inventoryRequirements.Length; ++i)
                RemoveFromInventory(_currentItem.inventoryRequirements[i]);

            _currentItem.Interact();
        }
    }


    //  INVENTORY
    private void AddToInventory(InteractableItem item)
    {
        _inventory.Add(item);
    }

    private void RemoveFromInventory(InteractableItem item)
    {
        _inventory.Remove(item);
    }

    private bool HasInInventory(InteractableItem item)
    {
        return _inventory.Contains(item);
    }

    public void IncrementDialogueLineOnButtonClick()
    {
        _currentNPC.Dialogue.IncrementDialogueLine();
        Debug.Log("Line incremented");
    }

    public void UpdateDialogueWithChoiceOnClick(int choice)
    {
        _currentNPC.Dialogue.UpdateDialogueWithChoice(choice);
        Debug.Log("Line incremented");
    }

    public void BoostOrderKarmaOnClick(int karmaBoost)
    {
        _player.PlayerKarma.OrderChoice(karmaBoost);
    }

    public void BoostFreedomKarmaOnClick(int karmaBoost)
    {
        _player.PlayerKarma.FreedomChoice(karmaBoost);
    }

    public void NeutralChoiceOnClick(int karmaLoss)
    {
        _player.PlayerKarma.NeutralChoice(karmaLoss);
    }
}
