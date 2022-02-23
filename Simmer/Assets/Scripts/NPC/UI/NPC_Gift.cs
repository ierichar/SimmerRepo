using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.NPC;

namespace Simmer.UI.NPC
{
    public class NPC_Gift : NPC_InterfaceWindow
    {
        public override void Construct(NPC_Manager npcManager)
        {
            _onChooseString = "ChooseGift";
            _onCloseString = "CloseGift";

            base.Construct(npcManager);
        }
    }
}