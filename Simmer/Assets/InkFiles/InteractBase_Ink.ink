VAR IS_RETURNING = 0
VAR IS_CORRECT_GIFT = 0
VAR IS_QUEST_COMPLETE = 0
VAR QUEST_ITEM = "QuestItem"
VAR QUEST_REWARD = "QuestReward"

>>> UpdateInkVar(IS_QUEST_COMPLETE, isQuestComplete);
>>> UpdateInkVar(QUEST_ITEM, questItem);
>>> UpdateInkVar(QUEST_REWARD, questReward);

>>> !CharEnter({CHARACTER_NAME})
>>> TextboxEnter(Default)

-> GreetingsGeneric ->
-> QuestText ->
-> InterfaceChoices

=== QuestText ===
{ IS_QUEST_COMPLETE:
- 0:
    -> QuestOngoing
- 1:
    -> QuestCompleted
}
->->

=== InterfaceChoices ===
->InteractOptions->
{ IS_QUEST_COMPLETE:
- 0:
    + [Give gift]
        -> ChooseGift
}
    + [Shop]
        -> ChooseShop
    
    + [Leave]
        -> EndGeneric


=== ChooseShop ===
-> OpenInterface("ChooseShop", -> EndGeneric, -> Continue)

=== ChooseGift ===
-> OpenInterface("ChooseGift", -> EndGeneric, -> CheckGift)

=== OpenInterface(ChooseEvent, -> EndKnot, -> AdditionalKnot)  ===
>>> TextboxExit();
>>> InvokeUnityEvent({ChooseEvent});
>>> WaitUntilEvent(CloseComplete);
>>> UpdateInkVar(IS_RETURNING, isReturning);
>>> UpdateInkVar(IS_CORRECT_GIFT, isCorrectGift);
>>> TextboxEnter(Default);
-> AdditionalKnot ->
-> Returning(EndKnot)

=== Returning(-> EndKnot)  ===
{ IS_RETURNING:
- 0:
    -> EndKnot
- 1:
    -> InterfaceChoices
}

=== CheckGift ===
>>> UpdateInkVar(IS_CORRECT_GIFT, isCorrectGift);
{ IS_CORRECT_GIFT:
- 1:
    -> PositiveGift
}
->->

=== Continue ===
->->

=== EndFunction ===
>>> !CharExit({CHARACTER_NAME});
-> END