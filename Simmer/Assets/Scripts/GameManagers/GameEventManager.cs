using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Stores game wide events during play
/// </summary>
public class GameEventManager : MonoBehaviour
{
    /// <summary>
    /// Click on player inventory item to highlight and display
    /// holding sprite
    /// </summary>
    public UnityEvent<int> onSelectItem = new UnityEvent<int>();

    /// <summary>
    /// Opening any UI window popup should invoke this event
    /// to disable player movement and controls
    /// </summary>
    public UnityEvent<bool> onInteractUI = new UnityEvent<bool>();

    public void Construct()
    {

    }

}
