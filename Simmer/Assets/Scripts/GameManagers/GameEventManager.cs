using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : MonoBehaviour
{
    public UnityEvent<int> OnSelectItem = new UnityEvent<int>();

    public void Construct()
    {

    }

}
