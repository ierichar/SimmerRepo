using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Interactable;
using Simmer.Player;


public class SplitStoveOven : MonoBehaviour
{
    [SerializeField] private GameObject UIButtons;
    [SerializeField] private GenericAppliance app1;
    [SerializeField] private GenericAppliance app2;

    [SerializeField] private SpriteRendererManager _highlightTarget;
    private InteractableBehaviour interactable;

    private int whichOpen;
    //0 then all closed, 1 then app1 open, 2 then app2 open 

    public void Construct(){
        UIButtons.SetActive(false);

        interactable = GetComponent<InteractableBehaviour>();
        interactable.Construct(ToggleInventory, _highlightTarget, true);

        whichOpen = 0;
    }

    public void ToggleInventory(){
        //print("tried to toggle on switch app");
        switch(whichOpen){
            case 0:
                UIButtons.SetActive(!UIButtons.activeSelf);
                break;
            case 1:
                app1.ToggleInventory();
                whichOpen = 0;
                break;
            case 2:
                app2.ToggleInventory();
                whichOpen = 0;
                break;
            default:
                print("switch statement failed out");
                break;
        }
    }

    //get called by button presses in Scene
    public void openApp1(){
        //print("openApp1 called");
        app1.ToggleInventory();
        UIButtons.SetActive(false);
        whichOpen = 1;
    }
    public void openApp2(){
        //print("openApp2 called");
        app2.ToggleInventory();
        UIButtons.SetActive(false);
        whichOpen = 2;
    }   

}
