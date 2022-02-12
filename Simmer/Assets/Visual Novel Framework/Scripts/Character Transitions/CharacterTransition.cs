using System.Collections;
using UnityEngine;

public abstract class CharacterTransition: ScriptableObject
{
    public abstract IEnumerator Co_EnterScreen(VN_Character character, MonoBehaviour caller);

    public abstract IEnumerator Co_ExitScreen(VN_Character character, MonoBehaviour caller);
}