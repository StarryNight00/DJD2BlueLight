using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField]    private string  objectName;
    
    public bool                 isInteractable;
    private bool                isClicked;

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void PrintName()
    {
        Debug.Log(objectName);
    }

    private void FixedUpdate()
    {
        if (isInteractable)
        {
            if (isClicked)
            {
                Debug.Log(objectName);
                Destroy(gameObject);
            }

            isClicked = false;
        }
    }
}
