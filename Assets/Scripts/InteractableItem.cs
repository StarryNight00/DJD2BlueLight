using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public enum InteractiveType { PICKABLE, INTERACT_ONCE, INTERACT_MULTIPLE, INDIRECT }

    public InteractiveType      type;
    public Texture              icon;
    public string               itemName;
    public string               requirementText;
    public string               interactionText;
    public bool                 isActive;

    public InteractableItem[]   interactionChain;
    public InteractableItem[]   activationChain;
    public InteractableItem[]   inventoryRequirements;

    private Animator _animator;

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
        _animator.SetTrigger("Interact");

        if (isActive)
        {
            InteractConnected();
            ProcessActivationChain();

            if (type == InteractableItem.InteractiveType.INTERACT_ONCE)
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
