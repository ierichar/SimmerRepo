using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;
using Simmer.Interactable;

public class PlateManager : GenericAppliance
{
    protected override void Finished()
    {
        base.Finished();
        _soundManager.PlaySound(6, false);
    }
}
