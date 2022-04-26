using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveManager : GenericAppliance
{
    protected override void Finished()
    {
        base.Finished();
        _soundManager.PlaySound(4, false);
    }
}
