VAR CHARACTER_NAME = "You"
>>> !CharEnter({CHARACTER_NAME});
VAR CHARACTER_NAME_1 = "Mom"
>>> !CharEnter({CHARACTER_NAME_1});
>>> TextboxEnter(Default)

{CHARACTER_NAME}: *Sigh*
{CHARACTER_NAME}: It’s been a week since I’ve moved here for work and I have no idea what’s in town.
{CHARACTER_NAME}: Oh, shoot… I forgot, I got that letter from my mom yesterday… Let's see…

* [Open Letter]
    -> Letter

=== Letter ===
{CHARACTER_NAME_1}: Hi Darling,
{CHARACTER_NAME_1}: I hope you’ve been enjoying your first few days in town. 
{CHARACTER_NAME_1}: I sent you some cookies like when we used to bake them when you were little.
{CHARACTER_NAME_1}: Remember?
{CHARACTER_NAME_1}: Also, I hear there's a fantastic cooking school in the area. You always mentioned how much you wanted to be a chef.
{CHARACTER_NAME_1}:Maybe you could take a few classes after making some
{CHARACTER_NAME_1}: Hope to hear from you soon.
{CHARACTER_NAME_1}: Love,
{CHARACTER_NAME_1}: Mom

{CHARACTER_NAME}: She's right...
{CHARACTER_NAME}: I need to get out more...
{CHARACTER_NAME}: Maybe I should check out the farmer's market in town. They'll probably have fresh ingredients there to start cooking.
>>> !CharExit({CHARACTER_NAME});
-> END