using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Inventory;
using Simmer.Items;
using Simmer.UI.Tooltips;
using Simmer.UI.RecipeMap;

namespace Simmer.UI
{
    public class MarketCanvasManager : PlayCanvasManager
    {
        public CanvasGroupManager canvasGroupManager { get; private set; }
        public Shop _shop;

        public override void Construct(
            GameEventManager gameEventManager
            , UISoundManager soundManager)
        {
            base.Construct(gameEventManager, soundManager);

            canvasGroupManager = GetComponent<CanvasGroupManager>();
            canvasGroupManager.Construct();

            _shop = FindObjectOfType<Shop>(true);
            _shop.Construct();
        }
    }
}