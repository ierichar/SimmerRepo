INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Bob"
VAR QUEST_ITEM = "Cream Cake"
VAR QUEST_REWARD = "Baked Chicken"

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hello there.
->->

=== QuestText ===
{ IS_QUEST_COMPLETE:
- 0:
    {CHARACTER_NAME}: I wish I could eat some {QUEST_ITEM}. 
    {CHARACTER_NAME}: Here's what I know about the recipe.
- 1:
    {CHARACTER_NAME}: I really like this {QUEST_ITEM}!
}
->->

=== EndShop ===
{CHARACTER_NAME}: Pleasure doing business!
-> EndFunction

=== PositiveGift ===
>>> UpdateInkVar(IS_CORRECT_GIFT, isCorrectGift);
{ IS_CORRECT_GIFT:
- 1:
    {CHARACTER_NAME}: I really wanted this! Thanks!
    {CHARACTER_NAME}: Please take this {QUEST_REWARD} recipe as a reward.
}
->->

=== EndGift ===
{CHARACTER_NAME}: Thanks!
-> EndFunction

=== EndGeneric ===
{CHARACTER_NAME}: Have a nice day!
-> EndFunction