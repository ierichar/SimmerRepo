using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.NPC;

namespace Simmer.UI.NPC
{
    public class NPC_Shop : NPC_InterfaceWindow
    {
        public Shop shop { get; private set; }

        public override void Construct(NPC_Manager npcManager)
        {
            _onChooseString = "ChooseShop";
            _onCloseString = "CloseShop";

            base.Construct(npcManager);

            shop = gameObject.FindChildObject<Shop>();
        }

        protected override void OnOpenCallback(NPC_Data npc_data)
        {
            print(this + " OnOpenCallback");
            base.OnOpenCallback(npc_data);
            shop.ConstructShopButtons(npc_data);
        }
    }
}