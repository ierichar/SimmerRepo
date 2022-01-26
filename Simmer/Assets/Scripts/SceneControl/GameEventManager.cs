using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Items;

public class GameEventManager : MonoBehaviour
{
    public UnityEvent<ItemBehaviour> OnSelectItem
        = new UnityEvent<ItemBehaviour>();

    public void Construct()
    {

    }

}
