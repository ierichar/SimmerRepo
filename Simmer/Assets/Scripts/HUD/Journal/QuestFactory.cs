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

    public void Construct()
    {
        GlobalPlayerData.OnActiveQuestsUpdated.AddListener(UpdateJournal);
        UpdateJournal();
    }

    private void OnDestroy()
    {
        GlobalPlayerData.OnActiveQuestsUpdated.RemoveListener(UpdateJournal);
    }

    public void UpdateJournal()
    {
        foreach (GameObject obj in children)
        {
            Destroy(obj);
        }

        foreach (var pair in GlobalPlayerData.activeQuestDictionary)
        {
            GameObject newItem = Instantiate(questPrefab, this.transform);
            children.Add(newItem);

            string finalQuest = "";
            finalQuest += pair.Key.name + ": Wants "
                + pair.Value.questItem.name;
            newItem.GetComponentInChildren<UITextManager>().Construct();
            newItem.GetComponentInChildren<UITextManager>().SetText(finalQuest);
        }
    }
    
}
