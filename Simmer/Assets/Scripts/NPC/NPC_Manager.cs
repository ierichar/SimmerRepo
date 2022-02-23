using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using Simmer.VN;
using Simmer.UI;
using Simmer.UI.NPC;

namespace Simmer.NPC
{
    public class NPC_Manager : MonoBehaviour
    {
        public VN_Manager vn_manager { get; private set; }
        public VN_SharedVariables vn_sharedVariables { get; private set; }
        private MarketCanvasManager _marketCanvasManager;
        private CanvasGroupManager _playCanvasGroupManager;

        [SerializeField] private float _playCanvasFadeDuration;
        [SerializeField] private Ease _playCanvasFadeEase;

        private NPC_Shop _npcShop;
        private NPC_Gift _npcGift;

        private List<NPC_Behaviour> _allNPCList =
            new List<NPC_Behaviour>();

        public UnityEvent<NPC_Data> OnNPCInteract
            = new UnityEvent<NPC_Data>();
        public UnityEvent OnCloseInterfaceCompleted
            = new UnityEvent();

        public NPC_InterfaceWindow targetInterfaceWindow;
        private bool _isInteracting;
        public NPC_Data currentNPC_Data { get; private set; }

        public void Construct(VN_Manager VNmanager
            , MarketCanvasManager marketCanvasManager)
        {
            vn_sharedVariables = VN_Util.manager.sharedVariables;

            vn_manager = VNmanager;
            _marketCanvasManager = marketCanvasManager;
            _playCanvasGroupManager = marketCanvasManager.canvasGroupManager;

            _npcShop = FindObjectOfType<NPC_Shop>(true);
            _npcShop.Construct(this);

            _npcGift = FindObjectOfType<NPC_Gift>(true);
            _npcGift.Construct(this);

            NPC_Behaviour[] npcArray = FindObjectsOfType<NPC_Behaviour>();
            foreach(var npc in npcArray)
            {
                _allNPCList.Add(npc);
                npc.Construct(this);
            }

            OnNPCInteract.AddListener(OnNPCInteractCallback);
            vn_manager.OnEndStory.AddListener(OnStopNPCInteract);

            VN_EventData closeInterfaceData =
                new VN_EventData(OnCloseInterfaceCompleted, "CloseComplete");
            vn_sharedVariables.AddEventData(closeInterfaceData);
        }

        public IEnumerator ShowInterfaceSequence()
        {
            targetInterfaceWindow.OnOpen.Invoke(currentNPC_Data);
            _marketCanvasManager.gameObject.SetActive(true);

            Tween fadeTween = _playCanvasGroupManager.Fade(1,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();
        }

        public IEnumerator HideInterfaceSequence()
        {
            print("isReturning: " + vn_sharedVariables.isReturning);

            Tween fadeTween = _playCanvasGroupManager.Fade(0,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();

            targetInterfaceWindow.gameObject.SetActive(false);
            _marketCanvasManager.gameObject.SetActive(false);
            
            targetInterfaceWindow = null;
            OnCloseInterfaceCompleted.Invoke();
        }

        private void OnNPCInteractCallback(NPC_Data npcData)
        {
            if (!_isInteracting
                && vn_manager.state == VN_Manager.VN_State.end)
            {
                StartCoroutine(InteractSequence(npcData));
            }
        }

        private IEnumerator InteractSequence(NPC_Data npcData)
        {
            _isInteracting = true;

            currentNPC_Data = npcData;

            Tween fadeTween = _playCanvasGroupManager.Fade(0,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();

            _marketCanvasManager.gameObject.SetActive(false);

            vn_manager.inkJSONAsset = npcData.npcInkAsset;
            vn_manager.StartStory();
        }

        private void OnStopNPCInteract()
        {
            StartCoroutine(StopInteractSequence());
        }

        private IEnumerator StopInteractSequence()
        {
            _marketCanvasManager.gameObject.SetActive(true);

            Tween fadeTween = _playCanvasGroupManager.Fade(1,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();
            
            currentNPC_Data = null;

            _isInteracting = false;
        }
    }
}