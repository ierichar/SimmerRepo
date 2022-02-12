using System.Collections;
using UnityEngine;

public abstract class TextboxTransition : ScriptableObject
{
    public abstract IEnumerator Co_EnterScreen(VN_Manager manager, MonoBehaviour caller);

    public abstract IEnumerator Co_ExitScreen(VN_Manager manager, MonoBehaviour caller);

}
