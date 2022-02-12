using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextboxExitCmd : MonoBehaviour, ICommandCall
{
    public IEnumerator Command(List<string> args)
    {
        VN_Manager manager = VN_Util.manager;
        TextboxData data = manager.textboxManager.data;

        yield return StartCoroutine(manager.textboxManager.HideTextbox(data));
    }
}
