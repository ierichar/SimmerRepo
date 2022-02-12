using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    public class CharExitCmd : MonoBehaviour, ICommandCall
    {
        public IEnumerator Command(List<string> args)
        {
            foreach (string arg in args)
            {
                CharacterData data = VN_Util.FindCharacterData(arg);
                VN_Character charObj = VN_Util.FindCharacterObj(data);

                yield return StartCoroutine(charObj.data.transition.Co_ExitScreen(charObj, charObj));
                charObj.ChangeSprite("");
                charObj.SetData(null);
            }
        }
    }
}