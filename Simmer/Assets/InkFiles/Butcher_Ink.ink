INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Missak"

=== FirstGreeting ===
{CHARACTER_NAME}: Hmmmm? Oh, are you new here?
Haha... I guess you could say that.
{CHARACTER_NAME}: Another cook looking to be more, eh?
How'd you-
{CHARACTER_NAME}: Well take it from me, I learned everything I
{CHARACTER_NAME}
->->

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

=== Rumor ===
{CHARACTER_NAME}: If you need the best eggs in town, talk to Mary.
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