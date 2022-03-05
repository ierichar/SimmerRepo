INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Cow Rancher"

=== GreetingsGeneric ===
{CHARACTER_NAME}: Howdy!
->->

=== QuestOngoing ===
{CHARACTER_NAME}: I have a real hankering for some {QUEST_ITEM}. Do you think you could make me some?
{CHARACTER_NAME}: Here's what I know about the recipe.
->->

=== QuestCompleted ===
{CHARACTER_NAME}: I can’t wait to eat this {QUEST_ITEM}!
->->

=== InteractOptions ===
{CHARACTER_NAME}: What do ya need?
->->

=== PositiveGift ===
{CHARACTER_NAME}: This is going to taste so delicious!
{CHARACTER_NAME}: I found this old recipe for {QUEST_REWARD}. Take it you should have more use for it than I do.
->->

=== EndGeneric ===
{CHARACTER_NAME}: Later partner!
-> EndFunction
