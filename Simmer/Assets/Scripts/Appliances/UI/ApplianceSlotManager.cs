using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;


public class ApplianceSlotManager : MonoBehaviour
{
    public List<SpawningSlotManager> slots;
    public void Construct(ItemFactory itemFactory)
    {

        SpawningSlotManager[] itemSlotArray
                = GetComponentsInChildren<SpawningSlotManager>();

            for (int i = 0; i < itemSlotArray.Length; ++i)
            {
                SpawningSlotManager thisSlot = itemSlotArray[i];

                slots.Add(thisSlot);
                thisSlot.Construct(itemFactory, i);
            }
    }
}
