using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for handling player interaction with items and NPCs.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    // private const that sets max interaction distance
    private const float MAX_INTERACTION_DISTANCE = 2.5f;
    // private InteractableItem used to store the item the player is 
    // interacting with currently
    private InteractableItem _currentItem;
    // private bool to verify if player has the requirements for an interaction
    private bool _hasRequirements;
    // private Transform that stores the Camera's transform
    private Transform _cameraTransform;
    // private list of InteractableItems, to store the items the player
    // posseses
    private List<InteractableItem> _inventory;
    // private Player that stores the Player instance used as the avatar
    private Player _player;
    // Serialized private CanvasManager variable
    [SerializeField] private CanvasManager _canvasManager;

    // private NPC used to store the NPC that the player is currently 
    // interacting with
    private NPC _currentNPC;

    /// <summary>
    /// Responsible for initializing the necessary variables on start of
    /// running time.
    /// </summary>
    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _currentItem = null;
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _inventory = new List<InteractableItem>();
        _canvasManager.SetCursorVisibility(false);
    }

    /// <summary>
    /// Responsible for updating the state of the object during runtime.
    /// </summary>
    private void Update()
    {
        CheckForInteractive();
        CheckForPlayerInteraction();
    }

    /// <summary>
    /// Responsible for checking if there are any interacble objects in range
    /// of the player
    /// </summary>
    private void CheckForInteractive()
    {
        if (Physics.Raycast(_cameraTransform.position,
                            _cameraTransform.forward,
                            out RaycastHit hitInfo,
                            MAX_INTERACTION_DISTANCE))
        {
            if (hitInfo.collider.TryGetComponent<InteractableItem>(
                out InteractableItem newItem))
            {
                CheckNewItem(newItem);
            }
            if (hitInfo.collider.TryGetComponent<NPC>(out NPC newNPC))
            {
                CheckNewNPC(newNPC);
            }
        }
        else _canvasManager.HideInteractionPanel();
    }

    /// <summary>
    /// Responsible for checking if an item caught by the raycast from the
    /// CheckForInteractive() method is capable of being interacted with at the
    /// time this method is called.
    /// </summary>
    /// <param name="newItem">InteractableItem caught by raycast</param>
    private void CheckNewItem(InteractableItem newItem)
    {
        if (newItem != null && newItem != _currentItem)
        {
            SetCurrentInteractive(newItem);
        }
        else if (newItem == null)
        {
            ClearCurrentInteractive();
        }
        else
        {
            ClearCurrentInteractive();
        }
    }

    /// <summary>
    /// Responsible for checking if an NPC caught by the raycast from the
    /// CheckForInteractive() method is capable of being interacted with at the
    /// time this method is called.
    /// </summary>
    /// <param name="newNPC">NPC caught by raycast</param>
    private void CheckNewNPC(NPC newNPC)
    {
        // if statement that checks if newNPC is not null and different from
        // _currentNPC, assigning newNPC to _currentNPC in case  conditions
        // are met
        if (newNPC != null && newNPC != _currentNPC)
        {
            SetCurrentNPC(newNPC);
        }
        // else if statement that DisplaysSpeech in case newNPC equals
        // _currentNPC
        else if (newNPC == _currentNPC)
        {
            DisplaySpeech();
        }
        // else statement that clears _currentNPC
        else
        {
            ClearCurrentNPC();
        }
    }

    /// <summary>
    /// Responsible for checking if player has the requirements to interact
    /// with newItem, setting it's value to _currentItem.
    /// </summary>
    /// <param name="newItem">InteractableItem that is not null and
    /// different from _currentItem</param>
    private void SetCurrentInteractive(InteractableItem newItem)
    {
        // Assign newItem's value to _currentItem
        _currentItem = newItem;
        if (HasInteractionRequirements())
        {
            // sets hasRequirements bool to true
            _hasRequirements = true;
            // shows interaction panel with interaction text
            _canvasManager.ShowInteractionPanel(_currentItem.interactionText);
        }
        else
        {
            // sets hasRequirements bool to false
            _hasRequirements = false;
            // shows interaction panel with requirement text
            _canvasManager.ShowInteractionPanel(_currentItem.requirementText);
        }
    }

    /// <summary>
    /// Responsible for rechecking if newNPC is not null, assigning it's
    /// value to _currentNPC in case it is not null, setting player's
    /// interaction state, setting cursor visibility to true and displaying
    /// speech.
    /// </summary>
    /// <param name="newNPC"></param>
    private void SetCurrentNPC(NPC newNPC)
    {
        // if statement that checks if newNPC is null
        if (newNPC != null)
        {
            // assign newNPC's value to _currentNPC
            _currentNPC = newNPC;
            // set player's interaction state to true
            _player.SetInteractionState(true);
            // set cursor visibility to true, so player can interact with
            // the interaction panel
            _canvasManager.SetCursorVisibility(true);
            // Display interaction panel and speech
            DisplaySpeech();
        }
    }

    /// <summary>
    /// Responsible for displaying the _currentNPC's Dialogue
    /// </summary>
    private void DisplaySpeech()
    {
        // show interaction panel with _currentNPC's current line of dialogue
        _canvasManager.ShowInteractionPanel(
            _currentNPC.Dialogue.Speech[_currentNPC.Dialogue.CurrentLine]);
        // show next line button
        _canvasManager.ShowNextButton();
        // hide choice panel
        _canvasManager.HideChoicePanel();
        // if statement that checks if the current line of dialogue is a line
        // where a choice exists
        if(_currentNPC.Dialogue.CurrentLine ==
            _currentNPC.Dialogue.ChoiceLines[
                _currentNPC.Dialogue.CurrentChoice])
        {
            // hide next buttom
            _canvasManager.HideNextButton();
            // show choice panel
            _canvasManager.ShowChoicePanel();
        }
        // if statement that checks if the current line of dialogue is one of
        // the three last lines
        if (_currentNPC.Dialogue.CurrentLine >=
            _currentNPC.Dialogue.Speech.Length - 3)
        {
            // hide next button
            _canvasManager.HideNextButton();
            // set player's interaction state to false
            _player.SetInteractionState(false);
            // set cursor visibility to false
            _canvasManager.SetCursorVisibility(false);
        }
    }

    /// <summary>
    /// Responsible for checking if the player has the requirements for the
    /// interaction with _currentItem
    /// </summary>
    /// <returns>Boolean that determines if player has requirments</returns>
    private bool HasInteractionRequirements()
    {
        // if statement that checks if _currentItem's requirement text is null
        // and returns true if it is
        if (_currentItem.requirementText == null)
            return true;
        // for loop that checks if any items in the player's inventory are the
        // required items for interaction and returns false in that case
        for (int i = 0; i < _currentItem.activationChain.Length; ++i)
            if (!HasInInventory(_currentItem.activationChain[i]))
                return false;
        
        return true;
    }

    /// <summary>
    /// Responsible for clearing the _currentItem variable
    /// </summary>
    private void ClearCurrentInteractive()
    {
        // assign null value to _currentItem
        _currentItem = null;
    }

    /// <summary>
    /// Responsible for clearing the _currentNPC variable
    /// </summary>
    private void ClearCurrentNPC()
    {
        // assign null value to _currentNPC
        _currentNPC = null;
    }

    /// <summary>
    /// Responsible for checking if player interacted with any interactable
    /// item.
    /// </summary>
    private void CheckForPlayerInteraction()
    {
        // if statement that checks if primary mouse button was pressed and
        // _currentItemd is not null
        if (Input.GetMouseButtonDown(0) && _currentItem != null)
        {
            // if statement that checks if _currentItem's type is PICKABLE
            if (_currentItem.type == InteractableItem.InteractiveType.PICKABLE)
                // picks up the item, adding to inventory
                Pick();
            else
                // interacts with item
                Interact();
        }
    }

    /// <summary>
    /// Responsible for adding item to the player's inventory and deactivating
    /// it in the scene.
    /// </summary>
    private void Pick()
    {
        // adds _currentItem to player's inventory
        AddToInventory(_currentItem);
        // set _currentItem's activeState to false
        _currentItem.gameObject.SetActive(false);
    }

    /// <summary>
    /// Responsible for interacting with an item. 
    /// </summary>
    private void Interact()
    {
        // if statement that checks if _hasRequirements bool is true
        if (_hasRequirements)
        {
            // for loop that removes any item from the inventory that is in
            // the _currentItem's required items for interaction
            for (int i = 0; i < _currentItem.inventoryRequirements.Length; ++i)
                RemoveFromInventory(_currentItem.inventoryRequirements[i]);
            // interact with _currentItem
            _currentItem.Interact();
        }
    }

    /// <summary>
    /// Responsible for adding item given as parameter to the player's
    /// inventory.
    /// </summary>
    /// <param name="item">Item to add to the player's inventory</param>
    private void AddToInventory(InteractableItem item)
    {
        // add given item to _inventory
        _inventory.Add(item);
    }

    /// <summary>
    /// Responsible for removing item given as parameter from the player's
    /// inventory.
    /// </summary>
    /// <param name="item">Item to remove from the player's inventory</param>
    private void RemoveFromInventory(InteractableItem item)
    {
        // remove item from _inventory
        _inventory.Remove(item);
    }

    /// <summary>
    /// Responsible for checking if player's inventory contains item given as
    /// parameter.
    /// </summary>
    /// <param name="item">Item to check if inventory contains</param>
    /// <returns>Returns true if inventory contains item and false if it
    /// doesn't</returns>
    private bool HasInInventory(InteractableItem item)
    {
        return _inventory.Contains(item);
    }

    /// <summary>
    /// Responsible for bridging the gap to the _currentNPC's Dialogue's 
    /// IncrementDialogue method, so it can be used in interaction panel
    /// buttons.
    /// </summary>
    public void IncrementDialogueLineOnButtonClick()
    {
        _currentNPC.Dialogue.IncrementDialogueLine();
    }
    /// <summary>
    /// Responsible for bridging the gap to the _currentNPC's Dialogue's 
    /// UpdateDialogueWithChoice method, so it can be used in interaction panel
    /// buttons.
    /// </summary>
    public void UpdateDialogueWithChoiceOnClick(int choice)
    {
        _currentNPC.Dialogue.UpdateDialogueWithChoice(choice);
    }
    /// <summary>
    /// Responsible for bridging the gap to the _player's PlayerKarma's 
    /// OrderChoice method, so it can be used in interaction panel
    /// buttons.
    /// </summary>
    public void BoostOrderKarmaOnClick(int karmaBoost)
    {
        _player.PlayerKarma.OrderChoice(karmaBoost);
    }
    /// <summary>
    /// Responsible for bridging the gap to the _player's PlayerKarma's 
    /// FreedomChoice method, so it can be used in interaction panel
    /// buttons.
    /// </summary>
    public void BoostFreedomKarmaOnClick(int karmaBoost)
    {
        _player.PlayerKarma.FreedomChoice(karmaBoost);
    }
    /// <summary>
    /// Responsible for bridging the gap to the _player's PlayerKarma's 
    /// NeutralChoice method, so it can be used in interaction panel
    /// buttons.
    /// </summary>
    public void NeutralChoiceOnClick(int karmaLoss)
    {
        _player.PlayerKarma.NeutralChoice(karmaLoss);
    }
}
