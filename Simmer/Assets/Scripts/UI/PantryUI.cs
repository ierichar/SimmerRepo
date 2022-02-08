/* Edited by Fernanda 2/7/22 to only open inventory UI when close to pantry sprite */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;
using Simmer.Interactable;

public class PantryUI : GenericAppliance
{
    private GameObject myInv;

    // private GraphicRaycaster graphicRaycaster; 
    public void Construct()
    {
        interactable = GetComponent<InteractableBehaviour>();
        SpriteRenderer highlightTarget = GetComponentInChildren<SpriteRenderer>();
        interactable.Construct(ToggleInventory, highlightTarget);

       // graphicRaycaster = GetComponent<GraphicRaycaster>();

       myInv = GameObject.Find("Pantry");
       myInv.SetActive(false);
       // graphicRaycaster.enabled = false;
    }

     public override void ToggleInventory()
    {
        if(!invOpen && !UI_OPEN){
            myInv.SetActive(true);
            invOpen = true;
            UI_OPEN = true;
        }else if(invOpen && UI_OPEN){
            myInv.SetActive(false);
            invOpen = false;
            UI_OPEN = false;
        }
    }
    public override void TryInteract(FoodItem item){

    }
    public override void AddItem(FoodItem recipe){

    }
    public override FoodItem TakeItem(){
        return null;
    }
    public override void ToggleOn(float duration){

    }
    protected override void Finished(){

    }
}
