using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public enum InteractiveType {PICKABLE, INTERACT_ONCE, INTERACT_MULTIPLE, INDIRECT};
    public InteractiveType      type;
    public string               interactionText;
    public Sprite               inventoryIcon;
    public string               inventoryName;
    public string               requirementText;
    public Interactive[]        activationChain;
    public Interactive[]        inventoryRequirements;
    public Interactive[]        interactionChain;
    private Animator            _animator;
    public bool                 isActive;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        isActive = true;
    }
    
    public void Interact()
    {
        if (_animator != null)
            _animator.SetTrigger("Interact");
        if (isActive)
        {
            ProcessInteractionChain();
            ProcessActivationChain();

            if (type == InteractiveType.INTERACT_ONCE)
                GetComponent<Collider>().enabled = false;
        }

        
    }

    public void ProcessInteractionChain()
    {
        if (interactionChain != null)
        {
            for (int i = 0; i < interactionChain.Length; ++i)
                interactionChain[i].Interact();
        }
    }

    public void ProcessActivationChain()
    {
        if (activationChain != null)
        {
            for (int i = 0; i < activationChain.Length; ++i)
                activationChain[i].Activate();
        }
    }
}
