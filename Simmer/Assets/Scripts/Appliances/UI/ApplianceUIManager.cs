using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
public class ApplianceUIManager : MonoBehaviour
{
    public ApplianceSlotManager slots;
    public void Construct(ItemFactory itemFactory){
        slots = gameObject.GetComponentInChildren<ApplianceSlotManager>();
        slots.Construct(itemFactory);
    }
}
