INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Bonnie"

=== FirstGreeting ===
{CHARACTER_NAME}: Howdy newcomer! I’ve been watchin’ you kick around the market and you’ve got the looks of a chef!
How did you -
{CHARACTER_NAME}: I spend a lot of time gazin’ out from under my brim and the comin’ and goin’ of 
{CHARACTER_NAME}: our market, you just start to see it after a while, and I see that you’ve got a
{CHARACTER_NAME}: discernin’ gaze. I’m {CHARACTER_NAME}, I bring the fruit of my family’s labor to the market here. 
->->

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
