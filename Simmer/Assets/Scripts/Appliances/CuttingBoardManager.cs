using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.Interactable;

public class CuttingBoardManager : GenericAppliance
{
    private bool cuttingStarted;
    private int numCuts;

    [SerializeField] private int numCutsMultiplier;

    public override void Construct(ItemFactory itemFactory){
        print("Calling derived class constructor from cutting board");
        interactable = GetComponent<InteractableBehaviour>();
        SpriteRenderer highlightTarget = GetComponentInChildren<SpriteRenderer>();
        interactable.Construct(ToggleInventory, highlightTarget);

        OnValidate.AddListener(base.OnValidateCallback);

        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _UIManager.Construct(itemFactory);
        _UIGameObject = _UIManager.gameObject;

        _UIGameObject.SetActive(false);

        _running = false;
        _finished = false;
        _timeRunning = 0.0f;

        _applianceSlotManager = _UIManager.slots.slots;

        //derived class variable init
        cuttingStarted = false;
        numCuts = 0;
    }

    public void chopping(){
        if(!cuttingStarted){
            Validation();

            if(_pendingTargetRecipe == null) 
                return;
            else 
                cuttingStarted = true;
        }else{
            if(numCuts >= _pendingTargetRecipe.baseActionTime * numCutsMultiplier){
                foreach(ItemSlotManager slot in _applianceSlotManager){
                    if(slot.currentItem != null) slot.EmptySlot();
                }
                Finished();
                numCuts = 0;
            }else{
                print("numCuts: " + numCuts);
                ++numCuts;
            }
        }
        
    }
}
