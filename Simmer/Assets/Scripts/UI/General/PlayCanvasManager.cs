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
    public class PlayCanvasManager : MonoBehaviour
    {
        public Canvas playCanvas { get; set; }

        public ItemFactory itemFactory { get; private set; }
        public InventoryUIManager inventoryUIManager { get; private set; }
        public RecipeMapWindow recipeMapWindow { get; private set; }

        public UnityEvent<int> OnSelectItem { get; private set; }

        public ScreenBlockManager screenBlockManager { get; private set; }

        public virtual void Construct(UnityEvent<int> OnSelectItem)
        {
            this.OnSelectItem = OnSelectItem;
            playCanvas = GetComponent<Canvas>();

            itemFactory = GetComponent<ItemFactory>();
            inventoryUIManager = GetComponentInChildren<InventoryUIManager>(true);
            recipeMapWindow = GetComponentInChildren<RecipeMapWindow>(true);
            screenBlockManager = GetComponentInChildren<ScreenBlockManager>();

            itemFactory.Construct(this);
            inventoryUIManager.Construct(itemFactory);
            recipeMapWindow.Construct();
            screenBlockManager.Construct();
        }
    }
}