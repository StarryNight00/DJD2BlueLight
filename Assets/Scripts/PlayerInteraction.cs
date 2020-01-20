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
    private LayerMask _vocalNPCsLayer;
    private LayerMask _interactablesLayer;

    public CanvasManager canvasManager;

    // ---------------------------TEST CODE HERE-------------------------------
    private NPC _currentNPC;
    private bool _mouseClicked;

    private void Start()
    {
        _vocalNPCsLayer = LayerMask.NameToLayer("VocalNPCs");
        _vocalNPCsLayer = LayerMask.NameToLayer("Interactables");
        _currentItem = null;
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _inventory = new List<InteractableItem>();
    }
    private void Update()
    {
        Debug.Log("Begin update");
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
        else canvasManager.HideInteractionPanel();
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
        else if (newNPC == null)
        {
            Debug.Log("New interactive NPC is invalid as it is null");
            ClearCurrentNPC();
        }
        else
        {
            Debug.Log("New interactive NPC is invalid for unknown reasons");
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
            canvasManager.ShowInteractionPanel(_currentItem.interactionText);
        }
        else
        {
            Debug.Log("Item interaction requirements not met; Displaying" +
                "requirements");
            _hasRequirements = false;
            canvasManager.ShowInteractionPanel(_currentItem.requirementText);
        }
    }

    // -----------------------------TEST CODE HERE-----------------------------
    private void SetCurrentNPC(NPC newNPC)
    {
        if (newNPC != null)
        {
            _currentNPC = newNPC;

            Debug.Log("Current interactive NPC set, displaying dialogue");
            canvasManager.ShowInteractionPanel(
                _currentNPC.Dialogue.Speech[_currentNPC.Dialogue.CurrentLine]);
            Debug.Log("Line displayed");

            //if(Input.GetMouseButtonDown(0)) 
            //{
            //    _currentNPC.Dialogue.IncrementDialogueLine();
            //    Debug.Log("Dialogue line updated");
            //}
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
        //canvasManager.HideInteractionPanel();
    }

    private void ClearCurrentNPC()
    {
        _currentNPC = null;
        //canvasManager.HideInteractionPanel();
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
}
