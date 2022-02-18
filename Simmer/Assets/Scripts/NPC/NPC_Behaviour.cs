using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ink.Runtime;

using Simmer.Interactable;
using Simmer.VN;

namespace Simmer.NPC
{
    public class NPC_Behaviour : MonoBehaviour
    {
        private InteractableBehaviour _interactableBehaviour;

        private NPC_Manager _npcManager;

        [SerializeField] private NPC_Data npcData;

        public void Construct(NPC_Manager npcManager)
        {
            _npcManager = npcManager;

            _interactableBehaviour = GetComponent<InteractableBehaviour>();
            _interactableBehaviour.Construct(OnInteractCallback, null);
        }

        private void OnInteractCallback()
        {
            _npcManager.OnNPCInteract.Invoke(npcData.npcInkAsset);
        }
    }
}