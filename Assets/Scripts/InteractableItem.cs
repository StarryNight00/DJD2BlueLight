using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for the interactions allowed with items.
/// </summary>
public class InteractableItem : MonoBehaviour
{
    // Serialized private _type variable of InteractiveType type
    [SerializeField] private InteractiveType    _type;
    // Serialized private _itemName variable of string type
    [SerializeField] private string             _itemName;
    // Serialized private _requirementText variable of string type
    [SerializeField] private string             _requirementText;
    // Serialized private _interactionText variable of string type
    [SerializeField] private string             _interactionText;
    // Serialized private _isActive variable of bool type
    [SerializeField] private bool               _isActive;

    // Serialized private _animator variable of Animator type
    private Animator _animator;

    // public InteractableItem array interactionChain
    public InteractableItem[]   interactionChain;
    // public InteractableItem array activationChain
    public InteractableItem[]   activationChain;
    // public InteractableItem array inventoryRequirement
    public InteractableItem[]   inventoryRequirements;

    // public RequirementText property of string type
    public string RequirementText => _requirementText;
    // public InteractionText property of string type
    public string InteractionText => _interactionText;
    // public Type property of InteractiveType type
    public InteractiveType Type => _type;

    /// <summary>
    /// Responsible for initializing the necessary variables on start of
    /// running time.
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Responsible for activating an item.
    /// </summary>
    private void Activate()
    {
        // set _isActive to true
        _isActive = true;
    }

    /// <summary>
    /// Responsible for the interaction logic of an item.
    /// </summary>
    public void Interact()
    {
        // if statement that checks if _animator is null
        if (_animator != null)
            // in case _animator is not null, set trigger "Interact"
            _animator.SetTrigger("Interact");
        // if statement that checks if _isActive is true
        if (_isActive)
        {
            // in case _isActive is true, interact with connected items and
            // process the activation chain
            InteractConnected();
            ProcessActivationChain();
            // if statement that checks  if _type is 
            // InteractiveType.INTERACT_ONCE
            if (_type == InteractiveType.INTERACT_ONCE)
                // if it is, disable the item's collider
                GetComponent<Collider>().enabled = false;
        }
    }

    /// <summary>
    /// Responsible for interacting with connected items.
    /// </summary>
    private void InteractConnected()
    {
        // if statement that checks if interactionChain is null
        if (interactionChain != null)
        {
            // for loop that interacts with every item in the interactionChain
            for (int i = 0; i < interactionChain.Length; ++i)
                interactionChain[i].Interact();
        }
    }

    /// <summary>
    /// Responsible for processing the activation chain.
    /// </summary>
    private void ProcessActivationChain()
    {
        // if statement that checks if activationChain is null
        if (activationChain != null)
        {
            // for loop that activates every item in the activationChain
            for (int i = 0; i < activationChain.Length; ++i)
                interactionChain[i].Activate();
        }

    }
}
