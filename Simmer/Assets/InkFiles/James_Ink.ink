INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "James"

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hello there.
->->

=== InteractOptions ===
{CHARACTER_NAME}: What can I do for you?
->->

=== QuestOngoing ===
{CHARACTER_NAME}: I wish I could have some {QUEST_ITEM}. 
->->

=== QuestCompleted ===
{CHARACTER_NAME}: I really like this {QUEST_ITEM}!
->->

=== PositiveGift ===
{CHARACTER_NAME}: I really wanted this! Thanks!
{CHARACTER_NAME}: Please take this {QUEST_REWARD} recipe as a reward.
->->

=== EndGeneric ===
{CHARACTER_NAME}: Have a nice day!
-> EndFunction