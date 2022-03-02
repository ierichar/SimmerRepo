/* Edited by Fernanda 2/7/22 to only open inventory UI when close to pantry sprite */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Simmer.Interactable;
using Simmer.Items;

public class PantryUI : MonoBehaviour
{
    private InteractableBehaviour interactable;
    protected static bool UI_OPEN = false;
    private bool invOpen;
    [SerializeField] private GameObject UIGameObject;
    //[SerializeField] public ItemFactory itemFactory;

    public void Construct(ItemFactory itemFactory){
        interactable = GetComponent<InteractableBehaviour>();
        SpriteRendererManager highlightTarget
            = GetComponentInChildren<SpriteRendererManager>();
        highlightTarget.Construct();
        interactable.Construct(ToggleInventory, highlightTarget, true);

        PantrySlotGroupManager pantrySlots
            = FindObjectOfType<PantrySlotGroupManager>();
        pantrySlots.Construct(itemFactory);
        UIGameObject.SetActive(false);
        invOpen = false;
        UI_OPEN = false;
    }

    public void ToggleInventory(){
        Debug.Log("Toggle Inv Pantry");
        if(!invOpen){
            UIGameObject.SetActive(true);
            invOpen = true;
            UI_OPEN = true;
            Debug.Log("Open Pantry");
        }else if(invOpen){
            UIGameObject.SetActive(false);
            invOpen = false;
            UI_OPEN = false;
            Debug.Log("Close Pantry");
        }
    }

}
