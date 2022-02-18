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

        public override void Construct(UnityEvent<int> OnSelectItem, UISoundManager soundManager)
        {
            base.Construct(OnSelectItem, soundManager);

            canvasGroupManager = GetComponent<CanvasGroupManager>();
            canvasGroupManager.Construct();
        }
    }
}