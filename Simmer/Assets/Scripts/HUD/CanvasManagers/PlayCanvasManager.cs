using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Inventory;
using Simmer.Items;
using Simmer.UI.Tooltips;
using Simmer.UI.RecipeMap;
using Simmer.UI.RecipeBook;
using Simmer.UI.ImageQueue;

namespace Simmer.UI
{
    public class PlayCanvasManager : MonoBehaviour
    {
        public Canvas playCanvas { get; set; }

        public ItemFactory itemFactory { get; private set; }
        public InventoryUIManager inventoryUIManager { get; private set; }
        public RecipeMapWindow recipeMapWindow { get; private set; }
        public RecipeBookManager recipeBookManager { get; private set; }
        public MoneyUI moneyUI { get; private set; }
        public QuestFactory questFactory { get; private set; }
        public ImageQueueManager recipeBookQueueManager { get; private set; }
        public CenterQueueTrigger centerQueueTrigger { get; private set; }

        public UnityEvent<int> OnSelectItem { get; private set; }

        public ScreenBlockManager screenBlockManager { get; private set; }
        public UISoundManager soundManager {get; private set; }
        //public GenericAppliance[] applianceManager{ get; protected set;}

        public virtual void Construct(
            GameEventManager gameEventManager
            , UISoundManager soundManager)
        {
            this.OnSelectItem = gameEventManager.onSelectItem;
            playCanvas = GetComponent<Canvas>();

            itemFactory = GetComponent<ItemFactory>();
            inventoryUIManager = GetComponentInChildren<InventoryUIManager>(true);
            recipeMapWindow = GetComponentInChildren<RecipeMapWindow>(true);
            recipeBookManager = GetComponentInChildren<RecipeBookManager>(true);
            screenBlockManager = GetComponentInChildren<ScreenBlockManager>(true);
            moneyUI = GetComponentInChildren<MoneyUI>(true);
            questFactory = GetComponentInChildren<QuestFactory>(true);
            recipeBookQueueManager = gameObject.FindChildObject<ImageQueueManager>();
            centerQueueTrigger = GetComponentInChildren<CenterQueueTrigger>(true);

            this.soundManager = soundManager;

            itemFactory.Construct(this);
            inventoryUIManager.Construct(this);
            recipeMapWindow.Construct();
            recipeBookManager.Construct(gameEventManager);
            screenBlockManager.Construct();
            moneyUI.Construct();
            questFactory.Construct();
            recipeBookQueueManager.Construct();
            centerQueueTrigger.Construct(recipeBookQueueManager);
        }
    }
}