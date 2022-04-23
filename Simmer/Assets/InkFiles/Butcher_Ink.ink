INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Missak"

=== FirstGreeting ===
{CHARACTER_NAME}: Hmmmm? Oh, are you new here?
Haha... I guess you could say that.
{CHARACTER_NAME}: Another cook looking to be more, eh?
How'd you...?
{CHARACTER_NAME}: Well take it from me, I learned everything I about butchery, and there is no finer institution.
{CHARACTER_NAME}: I have the best cuts in town, so don't be a stranger.
->->

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hey.
->->

=== QuestStarted ===
{CHARACTER_NAME}: I wouldn't normally ask for this, but can you make me some {QUEST_ITEM}?
{CHARACTER_NAME}: I can give you a few headers about the recipe, but I'm not sure about the rest.
->->

=== QuestOngoing ===
{CHARACTER_NAME}: There's no shame checking your recipe book for reminders.
{CHARACTER_NAME}: A good chef uses all of the tools at their disposal... and salt.
->->

=== QuestCompleted ===
{CHARACTER_NAME}: Thank you for the {QUEST_ITEM}.
->->

=== Rumor ===
{CHARACTER_NAME}: If you want the best eggs in town, talk to Mary.
{CHARACTER_NAME}: I would use Taylor's vegetables for a beef stew everyday of the week.
{CHARACTER_NAME}: And Bonnie... she sells dairy.
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