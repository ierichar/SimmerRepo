using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;
using Simmer.Interactable;

public class MixerManager : GenericAppliance
{
    protected override void Finished()
    {
        base.Finished();
        _soundManager.PlaySound(2, false);
    }

    protected override void OnValidateCallbackPositive(){
        base.OnValidateCallbackPositive();
        _soundManager.PlaySound(3, false);
    }
    protected override void OnValidateCallbackNegative(){
        base.OnValidateCallbackNegative();

    }

}
