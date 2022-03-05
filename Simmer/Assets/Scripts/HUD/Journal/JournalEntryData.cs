using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.NPC;

public class JournalEntryData
{
    public NPC_Data npc { get; private set; }
    public NPC_QuestData quest { get; private set; }

    public JournalEntryData(NPC_Data npcData, NPC_QuestData questData){
        npc = npcData;
        quest = questData;
    }
}
