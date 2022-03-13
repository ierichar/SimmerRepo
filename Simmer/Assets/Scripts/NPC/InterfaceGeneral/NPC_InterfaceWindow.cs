using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.NPC;
using Simmer.VN;

namespace Simmer.NPC
{
    public abstract class NPC_InterfaceWindow : MonoBehaviour
    {
        protected NPC_Manager _npcManager;

        protected NPC_InterfaceExit _interfaceExit;
        protected NPC_InterfaceReturn _interfaceReturn;
        protected NPC_CharacterBox _characterBox;

        public UnityEvent OnChoose = new UnityEvent();
        public UnityEvent<NPC_Data> OnOpen = new UnityEvent<NPC_Data>();
        public UnityEvent OnClose = new UnityEvent();
        public UnityEvent OnReturn = new UnityEvent();

        protected string _onChooseString;
        protected string _onCloseString;
        public NPC_Data currentNPC_Data { get; private set; }

        public virtual void Construct(NPC_Manager npcManager)
        {
            _npcManager = npcManager;

            _interfaceExit = GetComponentInChildren
                <NPC_InterfaceExit>(true);
            _interfaceExit.Construct(this);

            _interfaceReturn = gameObject.FindChildObject
                <NPC_InterfaceReturn>();
            _interfaceReturn.Construct(this);

            _characterBox = gameObject.FindChildObject
                <NPC_CharacterBox>();
            _characterBox.Construct();

            OnChoose.AddListener(OnChooseCallback);
            OnOpen.AddListener(OnOpenCallback);
            OnClose.AddListener(OnCloseCallback);
            OnReturn.AddListener(OnReturnCallback);

            if (string.IsNullOrEmpty(_onChooseString))
            {
                Debug.LogError(this + " Error: _onChooseString cannot be empty");
            }

            if (string.IsNullOrEmpty(_onCloseString))
            {
                Debug.LogError(this + " Error: _onCloseString cannot be empty");
            }

            VN_EventData chooseData =
                new VN_EventData(OnChoose, _onChooseString);
            _npcManager.vn_sharedVariables.AddEventData(chooseData);
        }

        protected virtual void OnOpenCallback(NPC_Data npc_data)
        {
            currentNPC_Data = npc_data;
            _characterBox.SetCharacter(npc_data);
            gameObject.SetActive(true);
        }

        protected virtual void OnCloseCallback()
        {
            _npcManager.vn_sharedVariables.isReturning = 0;
            _npcManager.StartCoroutine(_npcManager
                .HideInterfaceSequence());
        }

        protected virtual void OnReturnCallback()
        {
            _npcManager.vn_sharedVariables.isReturning = 1;
            _npcManager.StartCoroutine(_npcManager
                .HideInterfaceSequence());
        }

        private void OnChooseCallback()
        {
            _npcManager.targetInterfaceWindow = this;
            _npcManager.StartCoroutine(_npcManager
                .ShowInterfaceSequence());
        }
    }
}