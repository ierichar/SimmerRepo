INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Bonnie"

=== FirstGreeting ===
{CHARACTER_NAME}: Howdy newcomer! I’ve been watchin’ you kick around the market and you’ve got the looks of a chef!
How did you...
{CHARACTER_NAME}: I spend a lot of time gazin’ out from under my brim and the comin’ and goin’ of our market, you just start to see it after a while...
{CHARACTER_NAME}: And I see that you’ve got a discernin’ gaze. I’m {CHARACTER_NAME}, I bring the fruit of my family’s labor to the market here. 
->->

=== GreetingsGeneric ===
{CHARACTER_NAME}: Howdy!
->->

=== QuestStarted ===
{CHARACTER_NAME}: I have a real hankering for some {QUEST_ITEM}. Do you think you could make me some?
It might take some time but...
{CHARACTER_NAME}: Great! Here's what I know about the recipe.
->->

=== QuestOngoing ===
{CHARACTER_NAME}: If you need help making {QUEST_ITEM}, keep checking with the other clerks and see what people have in stock.
{CHARACTER_NAME}: You may find the ingredient you need or something to experiment with.
->->

=== QuestCompleted ===
{CHARACTER_NAME}: I can’t wait to eat this {QUEST_ITEM}!
->->

=== Rumor ===
{CHARACTER_NAME}: *mumble* Did Missak put oil in his beard...?
{CHARACTER_NAME}: Oh! Haha... didn't see you there... I didn't say nothing... you hear!?
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
