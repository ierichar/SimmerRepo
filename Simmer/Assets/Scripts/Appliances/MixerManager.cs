using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;

public class MixerManager : GenericAppliance
{
    private List<ItemSlotManager> _inventorySlotManagerList
            = new List<ItemSlotManager>();
    
    private GameObject myInv;
    private GameObject mixerSlots;

    public void Construct(ItemFactory itemFactory)
    {
        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _invSize = 6;
        myInv = GameObject.Find("MixerInventory");
        mixerSlots = GameObject.Find("MixerSlots");
        myInv.SetActive(false);
        

        //_idle = true;
        _running = false;
        _finished = false;

        _timeRunning = 0.0f;

        // Will get them in order of Scene Hierarchy from top to bottom
        ItemSlotManager[] itemSlotArray
            = mixerSlots.GetComponentsInChildren<ItemSlotManager>();

        for (int i = 0; i < _invSize; ++i)
        {
            ItemSlotManager thisSlot = itemSlotArray[i];

            _inventorySlotManagerList.Add(thisSlot);
            thisSlot.Construct(itemFactory, i);
        }
    }
    
    void FixedUpdate()
    {
        //update time running
        if(_running){
            _timeRunning += Time.deltaTime;
            //Debug.Log("Time: " + _timeRunning);
            
        }if(!_running){
            _timeRunning = 0.0f;
        }
    }

    public override void ToggleInventory(){
        if(!invOpen){
            myInv.SetActive(true);
            invOpen = true;
        }else{
            myInv.SetActive(false);
            invOpen = false;
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
