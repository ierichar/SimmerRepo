INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Taylor"

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hey, good to see you!
->->

=== FirstGreeting ===
{CHARACTER_NAME}: Hey there! You’re new to the market, right? I'm {CHARACTER_NAME}. I recognize all of the regulars and you ain’t one of 'em!
I just moved here. I'm hoping to get into the local culinary school.
{CHARACTER_NAME}: So you’re like a chef then huh? That’s so much fun! 
{CHARACTER_NAME}: I’ve eaten nothing but carrot and beet salad for three days! I want something *sweet*.
->->

// --------------------------------------------------------------------
// Stage 1 Quest Line
// --------------------------------------------------------------------

=== QuestStarted_1 ===
{CHARACTER_NAME}: Do you think you could make me some {QUEST_ITEM}?
...
{CHARACTER_NAME}: Great! I’m so excited! You can definitely find everything you need right here at the market. Buying locally is always best!
->->

=== QuestOngoing_1 ===
{CHARACTER_NAME}: I'm super excited for {QUEST_ITEM}!
{CHARACTER_NAME}: Don't forget to sleep if it gets too late.
->->

=== QuestCompleted_1 ===
{CHARACTER_NAME}: Thanks for this {QUEST_ITEM}!
->->

// --------------------------------------------------------------------
// Stage 2 Quest Line
// --------------------------------------------------------------------

== QuestStarted_2 ===
{CHARACTER_NAME}: Listen, if you're trying to get better at baking, I have this... friend... that really likes {QUEST_ITEM}.
Haha, no judement here. I'm sure I can try to make them, I just need the recipe.
{CHARACTER_NAME}: Of course, but again, I don't want them... they're for my friend... Baylor...
Of course, {QUEST_ITEM} for your friend Baylor. Got it.
->->

=== QuestOngoing_2 ===
{CHARACTER_NAME}: Hey, Baylor says no rush, but they told me to tell you that their sweet tooth is acting up.
->->

=== QuestCompleted_2 ===
{CHARACTER_NAME}: AAAAAAAAAAAAAAHHHHHHH!
Woah, are you okay!?
{CHARACTER_NAME}: Yes, yes, haha these {QUEST_ITEM} are going to make me- I mean...
{CHARACTER_NAME}: Baylor...
{CHARACTER_NAME}: So happy!
{CHARACTER_NAME}: Thank you, thank you! Here's some information on {QUEST_REWARD} that I had in an old recipe book.
->->

// --------------------------------------------------------------------
// Generic Voice Lines
// --------------------------------------------------------------------

=== ClosedMorning ===
{CHARACTER_NAME}: Sorry, I'm still getting ready. I open up shop soon.
->->

=== ClosedNight ===
{CHARACTER_NAME}: Shoot, I just packed up shop. I'll be open tomorrow morning though!
->->

=== GenericComment ===
{CHARACTER_NAME}: It's a beautiful day today, isn't it?
->->

=== Rumor ===
{CHARACTER_NAME}: I heard Missak mumbling about something earlier. I wonder what's bothering him.
->->

=== InteractOptions ===
{CHARACTER_NAME}: Anyways, what do you need?
->->

=== PositiveGift ===
{CHARACTER_NAME}: Thanks, these look great! Let me try one… 
{CHARACTER_NAME}: *nom*
{CHARACTER_NAME}: WOW! These are amazing. Where did you learn to make them?
...
{CHARACTER_NAME}: Your mom’s recipe you say? She has a great recipe and you have a chef’s intuition.
{CHARACTER_NAME}: Here, I bet you'd be able to make this {QUEST_REWARD} no problem.
Thanks!
{CHARACTER_NAME}: Y’know, I just got our latest yeast culture going, come back tomorrow and I should have some in stock and you can start baking your own bread!
->->

=== EndGeneric ===
{CHARACTER_NAME}: See you later!
-> EndFunction