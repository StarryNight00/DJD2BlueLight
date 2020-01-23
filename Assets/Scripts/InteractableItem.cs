using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for the interactions allowed with items.
/// </summary>
public class InteractableItem : MonoBehaviour
{

    [SerializeField] private InteractiveType    type;
    [SerializeField] private Sprite             icon;
    [SerializeField] private string             itemName;
    [SerializeField] private string             requirementText;
    [SerializeField] private string             interactionText;
    [SerializeField] private bool               isActive;

    public InteractableItem[]   interactionChain;
    public InteractableItem[]   activationChain;
    public InteractableItem[]   inventoryRequirements;

    private Animator            _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Activate()
    {
        isActive = true;
    }

    public void Interact()
    {
        if (_animator != null)
            _animator.SetTrigger("Interact");

        if (isActive)
        {
            InteractConnected();
            ProcessActivationChain();

            if (type == InteractiveType.INTERACT_ONCE)
                GetComponent<Collider>().enabled = false;
        }
    }

    private void InteractConnected()
    {
        if (interactionChain != null)
        {
            for (int i = 0; i < interactionChain.Length; ++i)
                interactionChain[i].Interact();
        }
    }

    private void ProcessActivationChain()
    {
        if (activationChain != null)
        {
            for (int i = 0; i < activationChain.Length; ++i)
                interactionChain[i].Activate();
        }

    }
}
