using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;

public class InteractSlot : MonoBehaviour
{
    public ItemSlotManager itemSlot;
    // Start is called before the first frame update
    public void Construct()
    {
        itemSlot = GetComponentInChildren<ItemSlotManager>();
        itemSlot.Construct(0);
    }
}
