VAR IS_RETURNING = 0
VAR IS_CORRECT_GIFT = 0
VAR IS_QUEST_STARTED = 0
VAR IS_QUEST_COMPLETE = 0
VAR CURRENT_STAGE = "CurrentStage"
VAR INTERACTIONS = 0
VAR QUEST_STARTED = 0
VAR QUEST_ITEM = "QuestItem"
VAR QUEST_REWARD = "QuestReward"
VAR IS_CLOSED_MORNING = 0
VAR IS_CLOSED_NIGHT = 0

>>> UpdateInkVar(IS_CLOSED_MORNING, isClosedMorning);
>>> UpdateInkVar(IS_CLOSED_NIGHT, isClosedNight);
>>> UpdateInkVar(IS_QUEST_STARTED, isQuestStarted);
>>> UpdateInkVar(IS_QUEST_COMPLETE, isQuestComplete);
>>> UpdateInkVar(CURRENT_STAGE, currentStage);
>>> UpdateInkVar(INTERACTIONS, interactionCount);
>>> UpdateInkVar(QUEST_ITEM, questItem);
>>> UpdateInkVar(QUEST_REWARD, questReward);

>>> !CharEnter({CHARACTER_NAME})
>>> TextboxEnter(Default)

-> GreetingText ->
-> QuestText ->
-> InterfaceChoices

=== GreetingText ===
{ INTERACTIONS:
- 0:
    -> FirstGreeting
- else:
    -> GreetingsGeneric
}

=== QuestText ===
{ CURRENT_STAGE:
- 0:
    { IS_QUEST_STARTED:
    - 0:
        -> Rumor_1
    - 1:
        -> QuestStarted_1
    - 2:
        { IS_QUEST_COMPLETE:
        - 0:
            -> QuestOngoing_1
        - 1:
            -> QuestCompleted_1
        }
    }
- 1:
    { IS_QUEST_STARTED:
    - 0:
        -> Rumor_1
    - 1:
        -> QuestStarted_1
    - 2:
        { IS_QUEST_COMPLETE:
        - 0:
            -> QuestOngoing_1
        - 1:
            -> QuestCompleted_1
        }
    }
- 2:
    { IS_QUEST_STARTED:
    - 0:
        -> Rumor_2
    - 1:
        -> QuestStarted_2
    - 2:
        { IS_QUEST_COMPLETE:
        - 0:
            -> QuestOngoing_2
        - 1:
            -> QuestCompleted_2
        }
    }
}
->->

=== InterfaceChoices ===
->InteractOptions->
    { IS_QUEST_STARTED:
    - 1:
        { IS_QUEST_COMPLETE:
        - 0:
            + [Give gift]
                -> ChooseGift
        }
    - 2:
        { IS_QUEST_COMPLETE:
        - 0:
            + [Give gift]
                -> ChooseGift
        }
    }
    + [Shop]
        -> ChooseShop
    
    + [Leave]
        -> EndGeneric


=== ChooseShop ===
{ IS_CLOSED_MORNING:
- 0:
    { IS_CLOSED_NIGHT:
    - 0:
        -> OpenInterface("ChooseShop", -> EndGeneric, -> Continue)
    - 1:
        -> ClosedNight -> InterfaceChoices
    }
- 1:
    -> ClosedMorning -> InterfaceChoices
}

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
    { CURRENT_STAGE:
    - 0:
        -> PositiveGift_1
    - 1:
        -> PositiveGift_1
    - 2:
        -> PositiveGift_2
    }
}
->->

=== Continue ===
->->

=== EndFunction ===
>>> !CharExit({CHARACTER_NAME});
-> END