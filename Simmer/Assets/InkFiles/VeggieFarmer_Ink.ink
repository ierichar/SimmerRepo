INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Taylor"

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hey, good to see you!
->->

=== FirstGreeting ===
{CHARACTER_NAME}: Hey there! You’re new to the market, right? I recognize all of the regulars and you ain’t one of em!
I just moved here. I'm hoping to get into the local culinary school.
{CHARACTER_NAME}: So you’re like a chef then huh! 
{CHARACTER_NAME}: That’s so much fun! 
{CHARACTER_NAME}: I’ve eaten nothing but carrot and beet salad for three days! I want something *sweet*.
{CHARACTER_NAME}: Do you think you could make me some sugar cookies?
...
{CHARACTER_NAME}: Great! I’m so excited! You can definitely find everything you need right here at the market. Buying locally is always best!
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
{CHARACTER_NAME}: Thanks, these look great! Let me try one… 
{CHARACTER_NAME}: *nom*
{CHARACTER_NAME}: WOW! These are amazing. Where did you learn to make them?
...
{CHARACTER_NAME}: Your mom’s recipe you say? She has a great recipe and you have a chef’s intuition.
{CHARACTER_NAME}: I bet you'd be able to make this {QUEST_REWARD} no problem.
->->

=== EndGeneric ===
{CHARACTER_NAME}: See you later!
-> EndFunction