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
{CHARACTER_NAME}: You know I haven't been able to make a good {QUEST_ITEM} before. 
{CHARACTER_NAME}: If you could help make me a really nice one, I'd be glad to share some of my family recipes.
That sounds great, I can do that!
{CHARACTER_NAME}: Fantastic! I can't wait!
->->

=== QuestOngoing ===
{CHARACTER_NAME}: I used to make {QUEST_ITEM} with my grandkids all the time.
{CHARACTER_NAME}: If you need to make a quick buck, most of the folks here are always willing to buy your homemade goods.
->->

=== QuestCompleted ===
{CHARACTER_NAME}: Thank you for this {QUEST_ITEM}, dearie!
->->

=== Rumor ===
{CHARACTER_NAME}: Some people aren't friendly right off the bat.
{CHARACTER_NAME}: Take the time to get know someone if you want to build a relationship.
{CHARACTER_NAME}: *whispers* Or give them some brownies!
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