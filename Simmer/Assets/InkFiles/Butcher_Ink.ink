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

// --------------------------------------------------------------------
// Stage 1 Quest Line
// --------------------------------------------------------------------

=== QuestStarted_1 ===
{CHARACTER_NAME}: I wouldn't normally ask for this, but can you make me some {QUEST_ITEM}?
{CHARACTER_NAME}: I can give you a few headers about the recipe, but I'm not sure about the rest.
->->

=== QuestOngoing_1 ===
{CHARACTER_NAME}: Don't rush on making the {QUEST_ITEM}.
{CHARACTER_NAME}: There's no shame checking your recipe book for reminders.
{CHARACTER_NAME}: A good chef uses all of the tools at their disposal... and salt.
->->

=== QuestCompleted_1 ===
{CHARACTER_NAME}: Thank you for the {QUEST_ITEM}.
->->

// --------------------------------------------------------------------
// Stage 2 Quest Line
// --------------------------------------------------------------------

=== QuestStarted_2 ===
{CHARACTER_NAME}: The audacity of that wannabe cowboy! Charging this much for a side of beef!
{CHARACTER_NAME}: I’ll have to charge an arm and a leg to just keep the stand operating with these prices!
{CHARACTER_NAME}: If you’re here for beef today I’m afraid you won’t find it here now or anytime soon. I simply cannot afford what Bonnie is charging.
Well it wasn’t exactly cheap before…
{CHARACTER_NAME}: It wasn’t cheap because you can’t find better prepared meat in the entire city!
{CHARACTER_NAME}: Argh! There’s nothing for it, I can’t afford it right now. If you want that to change you’ll have to go talk to “miss poncho” over there. 
{CHARACTER_NAME}: What’s it for? It doesn’t even rain here!
->->

=== QuestOngoing_2 ===
{CHARACTER_NAME}: I don't know why Bonnie has to be so stubborn...
->->

=== QuestCompleted_2 ===
{CHARACTER_NAME}: I got the invoice from Bonnie, that’s a better price on beef than I could have ever bartered! 
{CHARACTER_NAME}: How’d you manage this?
I was courteous and I offered to help…
{CHARACTER_NAME}: Are you saying I’m not sweet-talkin’ enough or something?
{CHARACTER_NAME}: Doesn’t matter. The important thing is I get to go back to being the best butcher in town!
{CHARACTER_NAME}: I’ll get a side in and get it cut up for sale right away. Come back tomorrow and I can sell you some. 
->->

// --------------------------------------------------------------------

=== Rumor ===
{CHARACTER_NAME}: If you want the best eggs in town, talk to Mary.
{CHARACTER_NAME}: I would use Taylor's vegetables for a beef stew everyday of the week.
{CHARACTER_NAME}: And Bonnie... she sells dairy.
->->

=== InteractOptions ===
{CHARACTER_NAME}: Buying anything?
->->

=== PositiveGift ===
{CHARACTER_NAME}: Thanks. I appreciate this.
{CHARACTER_NAME}: Take this {QUEST_REWARD} recipe. It’s pretty good if you haven’t tried it.
->->

=== EndGeneric ===
{CHARACTER_NAME}: Bye.
-> EndFunction