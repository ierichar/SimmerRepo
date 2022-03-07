using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ink.Runtime;

using Simmer.Interactable;
using Simmer.UI.ImageQueue;

namespace Simmer.NPC
{
    public class NPC_Behaviour : MonoBehaviour
    {
        private NPC_Manager _npcManager;

        private InteractableBehaviour _interactableBehaviour;
        private NPC_Sprite _npcSprite;
        private SpriteRendererManager _highlightSprite;
        private QueueTrigger _queueTrigger;

        [SerializeField] private NPC_Data _npcData;

        public void Construct(NPC_Manager npcManager)
        {
            _npcManager = npcManager;

            _highlightSprite = GetComponentsInChildren<SpriteRendererManager>()[1];
            _highlightSprite.Construct();

            _interactableBehaviour = GetComponent<InteractableBehaviour>();
            _interactableBehaviour.Construct(
                OnInteractCallback, _highlightSprite, false);

            _npcSprite = GetComponentInChildren<NPC_Sprite>();
            _npcSprite.Construct();
            _npcSprite.SetSprite(_npcData.characterSprite);

            _queueTrigger = GetComponent<QueueTrigger>();
            _queueTrigger.Construct(npcManager.marketCanvasManager
                .recipeBookQueueManager);

        }

        private void OnInteractCallback()
        {
            _npcManager.onNPCInteract.Invoke(_npcData);
        }
    }
}