using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    public class CharEnterCmd : MonoBehaviour, ICommandCall
    {
        public IEnumerator Command(List<string> args)
        {
            foreach (string arg in args)
            {
                CharacterData data = VN_Util.FindCharacterData(arg);
                VN_Character charObj = VN_Util.FindEmptyCharObj(data);

                charObj.SetData(data);
                charObj.ChangeSprite(data.defaultSprite);

                yield return StartCoroutine(charObj.data.transition.Co_EnterScreen(charObj, charObj));
            }
        }
    }
}