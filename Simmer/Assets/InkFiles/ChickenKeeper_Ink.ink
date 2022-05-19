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

// --------------------------------------------------------------------
// Stage 1 Quest Line
// --------------------------------------------------------------------

=== Rumor_1 ===
{CHARACTER_NAME}: Some people aren't friendly right off the bat, you know.
{CHARACTER_NAME}: Take the time to get know someone if you want to build a relationship.
{CHARACTER_NAME}: *whispers* Or give them some brownies!
->->

== QuestStarted_1 ===
{CHARACTER_NAME}: You know I haven't been able to make a good {QUEST_ITEM} before. 
{CHARACTER_NAME}: If you could help make me a really nice one, I'd be glad to share some of my family recipes.
It may not come out like you remember, but I can take a shot at it!
{CHARACTER_NAME}: Fantastic! I can't wait!
->->

=== QuestOngoing_1 ===
{CHARACTER_NAME}: I used to make {QUEST_ITEM} with my grandkids all the time.
{CHARACTER_NAME}: Oh, and remember, if you need to make a quick buck, most of the folks here are always willing to buy your homemade goods.
->->

=== PositiveGift_1 ===
{CHARACTER_NAME}: I can't believe my nose... I bet it tastes as good as it smells. Well done!
{CHARACTER_NAME}: Please take this {QUEST_REWARD} recipe as a reward. it's not much, but hopefully this helps you on your journey towards chefdom.
->->

=== QuestCompleted_1 ===
{CHARACTER_NAME}: Thank you for the {QUEST_ITEM}, dearie!
->->

// --------------------------------------------------------------------
// Stage 2 Quest Line
// --------------------------------------------------------------------

=== Rumor_2 ===
{CHARACTER_NAME}: My bones are aching. Either that means a storm's coming, or I should just hydrate more often. Hehe!
->->

== QuestStarted_2 ===
{CHARACTER_NAME}: You know, if you're looking for a new challenging recipe, I have a dish that took me quite a while to master.
{CHARACTER_NAME}: My mom and her mom both taught me how to make {QUEST_ITEM} for cold winter nights.
{CHARACTER_NAME}: There never was a recipe, but I can give you a few ideas on the ingredients we used.
{CHARACTER_NAME}: While I know you don't have an open fire to cook with, your modern-day oven should work just fine!
->->

=== QuestOngoing_2 ===
{CHARACTER_NAME}: Make sure you follow a recipe's instructions step-by-step. Don't want to lose track of something and end up with charcoal.
->->

=== PositiveGift_2 ===
{CHARACTER_NAME}: Is that...?
{CHARACTER_NAME}: Mmmm I can smell it from here, that's sure is {QUEST_ITEM}!
{CHARACTER_NAME}: This takes me back to when I was a little girl. Please, at least let me compensate you for the ingredients.
{CHARACTER_NAME}: And don't let me forget, I did write down the recipe for {QUEST_REWARD}. I hope you make it for your loved ones someday just as my family did.
->->

=== QuestCompleted_2 ===
{CHARACTER_NAME}: I can tell you're becoming a better cook each time you come visit.
->->

// --------------------------------------------------------------------
// Generic Voice Lines
// --------------------------------------------------------------------

=== ClosedMorning ===
{CHARACTER_NAME}: I'm still getting my store set-up, one moment dear.
->->

=== ClosedNight ===
{CHARACTER_NAME}: I closed up shop for today, but there's always tomorrow.
->->

=== InteractOptions ===
{CHARACTER_NAME}: What can I do for you?
->->

=== EndGeneric ===
{CHARACTER_NAME}: Have a nice day!
-> EndFunction