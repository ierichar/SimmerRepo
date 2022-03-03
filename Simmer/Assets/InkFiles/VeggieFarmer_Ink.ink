INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Veggie Farmer"

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hey how are you doing!
->->

=== QuestOngoing ===
{CHARACTER_NAME}: I’m no good at baking, think you could make me a {QUEST_ITEM}?
{CHARACTER_NAME}: Here's what I know about the recipe.
->->

=== QuestCompleted ===
{CHARACTER_NAME}: Thanks for this {QUEST_ITEM}!
->->

=== InteractOptions ===
{CHARACTER_NAME}: So what do you need?
->->

=== PositiveGift ===
{CHARACTER_NAME}: This looks amazing! Thank you so much!
{CHARACTER_NAME}: I bet you'd be able to make this {QUEST_REWARD} no problem.
->->

=== EndGeneric ===
{CHARACTER_NAME}: See you later!
-> EndFunction