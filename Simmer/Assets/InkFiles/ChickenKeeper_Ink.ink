INCLUDE InteractBase_Ink.ink

VAR CHARACTER_NAME = "Mary"

=== FirstGreeting ===
{CHARACTER_NAME}: Oh! Good morning. I’m {CHARACTER_NAME}. Have I seen you around here before?
I live down the street, but this is my first time I've gone out to meet people.
I'm looking for some ingredients to inspire me.
{CHARACTER_NAME}: Ahhh an aspirant of the culinary artform. I do love seeing the youth getting after their dreams…
{CHARACTER_NAME}: Well, if that’s the case we’ll be making a friendly acquaintance quite often. 
{CHARACTER_NAME}: I retired some years ago and now I keep chickens as a bit of a hobby and make enough of a profit to keep this stand running.
{CHARACTER_NAME}: If you need any cooking tips or just a few eggs, feel free to stop by.
->->

=== GreetingsGeneric ===
{CHARACTER_NAME}: Hello young chef. What would you like today?
->->

== QuestStarted ===
{CHARACTER_NAME}: I wish I could have some {QUEST_ITEM}. 
{CHARACTER_NAME}: Here's what I know about the recipe.
->->

=== QuestOngoing ===
{CHARACTER_NAME}: I wish I could have some {QUEST_ITEM}. 
->->

=== QuestCompleted ===
{CHARACTER_NAME}: I really like this {QUEST_ITEM}!
->->

=== Rumor ===
{CHARACTER_NAME}: Some people aren't friendly right off the bat.
{CHARACTER_NAME}: Take the time to get know someone if you want to build a relationship.
{CHARACTER_NAME}: *whispers* or give them some brownies <3
->->

=== InteractOptions ===
{CHARACTER_NAME}: What can I do for you?
->->

=== PositiveGift ===
{CHARACTER_NAME}: I really wanted this! Thanks!
{CHARACTER_NAME}: Please take this {QUEST_REWARD} recipe as a reward.
->->

=== EndGeneric ===
{CHARACTER_NAME}: Have a nice day!
-> EndFunction