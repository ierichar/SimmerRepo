INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Butcher"

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hey.
->->

=== QuestOngoing ===
{CHARACTER_NAME}: Can you make me  some {QUEST_ITEM}?
{CHARACTER_NAME}: Here's what I know about the recipe.
->->

=== QuestCompleted ===
{CHARACTER_NAME}: Thank you for the {QUEST_ITEM}.
->->

=== InteractOptions ===
{CHARACTER_NAME}: Buying anything?
->->

=== PositiveGift ===
{CHARACTER_NAME}:  Thanks. I appreciate this.
{CHARACTER_NAME}: Take this {QUEST_REWARD} recipe. It’s pretty good if you haven’t tried it.
->->

=== EndGeneric ===
{CHARACTER_NAME}: Bye
-> EndFunction