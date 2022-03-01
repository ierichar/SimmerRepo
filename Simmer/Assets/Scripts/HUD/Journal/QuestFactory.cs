using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI;
using Simmer.NPC;

public class QuestFactory : MonoBehaviour
{
    [SerializeField] GameObject questPrefab;

    [SerializeField] NPC_Data npcData;
    [SerializeField] NPC_QuestData questData;

    List<GameObject> children = new List<GameObject>();

    public void Awake(){
        foreach(GameObject obj in children){
            Destroy(obj);
        }
        GlobalPlayerData.journalEntries.Add(new JournalEntryData(npcData, questData));
        GlobalPlayerData.journalEntries.Add(new JournalEntryData(npcData, questData));

        foreach(JournalEntryData entry in GlobalPlayerData.journalEntries){
            GameObject newItem = Instantiate(questPrefab, this.transform);
            children.Add(newItem);

            string finalQuest = "";
            finalQuest += entry.npc.name + ": Wants " + entry.quest.questIngredient.name;
            newItem.GetComponentInChildren<UITextManager>().Construct();
            newItem.GetComponentInChildren<UITextManager>().SetText(finalQuest);
        }
        
    }
    
}
