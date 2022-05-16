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

// --------------------------------------------------------------------
// Stage 1 Quest Line
// --------------------------------------------------------------------

=== QuestStarted_1 ===
{CHARACTER_NAME}: I have a real hankering for some {QUEST_ITEM}. Do you think you could make me some?
It might take some time but...
{CHARACTER_NAME}: Great! Here's what I know about the recipe.
->->

=== QuestOngoing_1 ===
{CHARACTER_NAME}: If you need help making {QUEST_ITEM}, keep checking with the other clerks and see what people have in stock.
{CHARACTER_NAME}: You may find the ingredient you need or something to experiment with.
->->

=== QuestCompleted_1 ===
{CHARACTER_NAME}: I can’t wait to chow down on this {QUEST_ITEM}!
->->

// --------------------------------------------------------------------
// Stage 2 Quest Line
// --------------------------------------------------------------------

== QuestStarted_2 ===
{CHARACTER_NAME}: Well you’re lookin' like a chef on a mission if i’ve ever seen one. Somethin’ I can help ya with?
Are you "Miss Poncho"? Missak asked me to come over and talk to you about the price of beef…
{CHARACTER_NAME}: He- EXCUSE ME?
{CHARACTER_NAME}: It keeps the sun and dust off of my nice clothes! It’s FUNCTIONAL!
I'm sorry, I thought he was calling you by your last name. Is there anything I can do to get-
{CHARACTER_NAME}: That Missak is just insufferable. I mean honestly! How long must he spend staring in the mirror of that chin fuzz he’s so proud of?
{CHARACTER_NAME}: Always strokin’ it all thoughtful like he were some big city intellectual!
I really just need...
{CHARACTER_NAME}: I just can’t stand him!
Look, I just need to find a way to get beef to market, can you help me out with this?
{CHARACTER_NAME}: ...
{CHARACTER_NAME}: I understand what I'm asking for beef is a lot, but the family business has just gotten harder and harder to run since 'Pa had to retire for his heart.
{CHARACTER_NAME}: So now it's just my younger brothers tending to the cattle and my lonesome self at the stand.
{CHARACTER_NAME}: *Sigh*
{CHARACTER_NAME}: Tell you what. 
{CHARACTER_NAME}: You make me {QUEST_ITEM} for my brothers, it'll give 'em some reinforcement and I'll figure out some new clients in the meantime.
->->

=== QuestOngoing_2 ===
{CHARACTER_NAME}: I really do appreciate you helping me. No pressure, but the boys are really excited!
->->

=== QuestCompleted_2 ===
{CHARACTER_NAME}: Thank you for this {QUEST_ITEM}, hun!
{CHARACTER_NAME}: I'll make sure you get what you need from Mr. Grouch.
Thank you!
->->

// --------------------------------------------------------------------
// Generic Voice Lines
// --------------------------------------------------------------------

=== ClosedMorning ===
{CHARACTER_NAME}: Hold your horses there, I'm not quite ready yet.
->->

=== ClosedNight ===
{CHARACTER_NAME}: I closed up shop for today, but there's always tomorrow.
->->

=== GenericComment ===
{CHARACTER_NAME}: Boy it sure is a hot one today, huh?
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
