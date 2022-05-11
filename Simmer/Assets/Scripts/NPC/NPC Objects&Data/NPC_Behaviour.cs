using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Ink.Runtime;

using Simmer.Interactable;
using Simmer.UI.ImageQueue;

namespace Simmer.NPC
{
    /// <summary>
    /// Stores instance data for an NPC. Construct initializes
    /// visuals and delegates interaction behaviour to NPC_Manager.
    /// </summary>
    public class NPC_Behaviour : MonoBehaviour
    {
        public NPC_Manager _npcManager { get; private set; }

        private InteractableBehaviour _interactableBehaviour;
        private NPC_Sprite _npcSprite;
        private SpriteRendererManager _highlightSprite;
        private QueueTrigger _queueTrigger;
        private Animator _animator;
        //private UnityAction stopInteractListenerAction;

        /// <summary>
        /// Data for this NPC. Cannot be null.
        /// </summary>
        [SerializeField] private NPC_Data _npcData;

        public void Construct(NPC_Manager npcManager)
        {
            _npcManager = npcManager;

            _highlightSprite = GetComponentsInChildren<SpriteRendererManager>()[1];
            _highlightSprite.Construct();

            //@@TheUnaverageJoe @@MPerez132 5/3/2022
            //---------------------------------------------------------------------
            _animator = GetComponentInChildren<Animator>();
            //stopInteractListenerAction += stopInteractionAnim;

            //---------------------------------------------------------------------

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

        //@@TheUnaverageJoe @@MPerez132 5/3/2022
        //---------------------------------------------------------------------
        private void OnInteractCallback()
        {
            _npcManager.onNPCInteract.Invoke(_npcData);
            if(_animator.HasState(0, Animator.StringToHash("Interaction"))){
                //Debug.Log("HAS INTERACTION STATE");
                _animator.SetBool("Interacting", true);
                _npcManager.vn_manager.OnEndStory.AddListener(stopInteractionAnim);
            }
            
        }
        private void stopInteractionAnim(){
            //Debug.Log("STOPPPING INTERACTGS");
            _animator.SetBool("Interacting", false);
            _npcManager.vn_manager.OnEndStory.RemoveListener(stopInteractionAnim);
        }
        //---------------------------------------------------------------------
        

        public NPC_Data GetNPC_Data() 
        {
            return _npcData;
        }
    }
}