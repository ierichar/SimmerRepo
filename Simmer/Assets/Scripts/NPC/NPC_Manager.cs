using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using Simmer.VN;
using Simmer.UI;

namespace Simmer.NPC
{
    public class NPC_Manager : MonoBehaviour
    {
        public VN_Manager vn_manager { get; private set; }
        private MarketCanvasManager _marketCanvasManager;
        private CanvasGroupManager _playCanvasGroupManager;

        [SerializeField] private float _playCanvasFadeDuration;
        [SerializeField] private Ease _playCanvasFadeEase;

        [SerializeField] private GameObject _npcInterface;
        [SerializeField] private Shop shop;

        private List<NPC_Behaviour> _allNPCList =
            new List<NPC_Behaviour>();

        public UnityEvent<TextAsset> OnNPCInteract
            = new UnityEvent<TextAsset>();

        public bool isInteractTransition;

        public void Construct(VN_Manager VNmanager
            , MarketCanvasManager marketCanvasManager)
        {
            vn_manager = VNmanager;
            _marketCanvasManager = marketCanvasManager;
            _playCanvasGroupManager = marketCanvasManager.canvasGroupManager;

            NPC_Behaviour[] npcArray = FindObjectsOfType<NPC_Behaviour>();
            foreach(var npc in npcArray)
            {
                _allNPCList.Add(npc);
                npc.Construct(this);
            }

            OnNPCInteract.AddListener(OnNPCInteractCallback);

            vn_manager.OnEndStory.AddListener(OnStopNPCInteract);
        }

        private void OnNPCInteractCallback(TextAsset npcInkAsset)
        {
            if (!isInteractTransition
                && vn_manager.state == VN_Manager.VN_State.end)
            {
                StartCoroutine(InteractSequence(npcInkAsset));
            }
        }

        private IEnumerator InteractSequence(TextAsset npcInkAsset)
        {
            isInteractTransition = true;

            Tween fadeTween = _playCanvasGroupManager.Fade(0,
                _playCanvasFadeDuration, _playCanvasFadeEase);

            yield return fadeTween.WaitForCompletion();

            _marketCanvasManager.gameObject.SetActive(false);

            vn_manager.inkJSONAsset = npcInkAsset;
            vn_manager.StartStory();

            isInteractTransition = false;
        }

        private void OnStopNPCInteract()
        {
            StartCoroutine(StopInteractSequence());
        }

        private IEnumerator StopInteractSequence()
        {
            isInteractTransition = true;

            _marketCanvasManager.gameObject.SetActive(true);

            Tween fadeTween = _playCanvasGroupManager.Fade(1,
                _playCanvasFadeDuration, _playCanvasFadeEase);

            yield return fadeTween.WaitForCompletion();

            _npcInterface.SetActive(true);

            isInteractTransition = false;
            shop.ToggleOn();
        }
    }
}