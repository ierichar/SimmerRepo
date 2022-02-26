using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : MonoBehaviour
{
    public UnityEvent<int> onSelectItem = new UnityEvent<int>();

    public UnityEvent<bool> onInteractUI = new UnityEvent<bool>();

    public void Construct()
    {

    }

}
