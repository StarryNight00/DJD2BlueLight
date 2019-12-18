using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private const float MAX_INTERACTION_DISTANCE = 2.5f;

    private InteractableItem _currentInteractive;
    private bool _hasRequirements;
    private Transform _cameraTransform;
    private List<InteractableItem> _inventory;

    public CanvasManager canvasManager;

    private void Start()
    {
        _currentInteractive = null;
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _inventory = new List<InteractableItem>();
    }
    private void Update()
    {
        CheckForInteractive();
        CheckForPlayerInteraction();
    }

    private void CheckForInteractive()
    {
        if (Physics.Raycast(_cameraTransform.position,
                            _cameraTransform.forward,
                            out RaycastHit hitInfo,
                            MAX_INTERACTION_DISTANCE))
        {
            InteractableItem newInteractive = hitInfo.
                             collider.GetComponent<InteractableItem>();

            if (newInteractive != null && newInteractive != _currentInteractive)
            {
                SetCurrentInteractive(newInteractive);
            }
            else if (newInteractive = null)
            {
                ClearCurrentInteractive();
            }
        }
        else
        {
            ClearCurrentInteractive();
        }
    }

    private void SetCurrentInteractive(InteractableItem newInteractive)
    {
        _currentInteractive = newInteractive;
        if (HasInteractionRequirements())
        {
            _hasRequirements = true;
            canvasManager.ShowInteractionPanel(_currentInteractive.interactionText);
        }
        else
        {
            _hasRequirements = false;
            canvasManager.ShowInteractionPanel(_currentInteractive.requirementText);
        }
    }

    private bool HasInteractionRequirements()
    {
        if (_currentInteractive.requirementText == null)
            return true;
        for (int i = 0; i < _currentInteractive.activationChain.Length; ++i)
            if (!HasInInventory(_currentInteractive.activationChain[i]))
                return false;

        return true;
    }

    private void ClearCurrentInteractive()
    {
        _currentInteractive = null;
        canvasManager.HideInteractionPanel();
    }

    private void CheckForPlayerInteraction()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteractive != null)
        {
            if (_currentInteractive.type == InteractableItem.InteractiveType.PICKABLE)
                Pick();
            else
                Interact();
        }
    }

    private void Pick()
    {
        AddToInventory(_currentInteractive);
        _currentInteractive.gameObject.SetActive(false);
    }

    private void Interact()
    {
        if (_hasRequirements)
        {
            for (int i = 0; i < _currentInteractive.inventoryRequirements.Length; ++i)
                RemoveFromInventory(_currentInteractive.inventoryRequirements[i]);

            _currentInteractive.Interact();
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
