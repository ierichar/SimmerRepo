using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

using Ink.Runtime;

using Simmer.Interactable;
using Simmer.VN;
using Simmer.Player;
using Simmer.UI;
using Simmer.NPC;

namespace Simmer.NPC
{
    public class MainMenu : MonoBehaviour
    {
        public NPC_Manager _npcManager;

        [SerializeField] private TextAsset npcInkAsset;

        public void Construct(NPC_Manager npcManager)
        {
            _npcManager = npcManager;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("EvanKitchen");
        }

        public void PlayTutorial()
        {
            _npcManager.OnNPCInteract.Invoke(npcInkAsset);
        }
    }
}