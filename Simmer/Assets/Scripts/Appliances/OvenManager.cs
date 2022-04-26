using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.FoodData;

public class OvenManager : GenericAppliance
{
    protected override void Finished()
    {
        base.Finished();
        _soundManager.PlaySound(2, false);
    }
}
