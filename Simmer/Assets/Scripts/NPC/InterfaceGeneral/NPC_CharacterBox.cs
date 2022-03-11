using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI;

namespace Simmer.NPC
{
    public class NPC_CharacterBox : MonoBehaviour
    {
        private NPC_Portrait portrait;
        private UITextManager nameText;

        public void Construct()
        {
            portrait = gameObject.FindChildObject
                <NPC_Portrait>();
            portrait.Construct();

            nameText = gameObject.FindChildObject
                <UITextManager>();
            nameText.Construct();
        }

        public void SetCharacter(NPC_Data data)
        {
            portrait.SetSprite(data.portraitSprite);
            nameText.SetText(data.name);
        }
    }
}