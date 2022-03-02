VAR IS_RETURNING = 0
VAR IS_CORRECT_GIFT = 1
VAR IS_QUEST_COMPLETE = 0

>>> !CharEnter({CHARACTER_NAME})
>>> TextboxEnter(Default)
-> GreetingsGeneric ->
-> QuestText ->
-> InterfaceChoices

=== InterfaceChoices ===
{CHARACTER_NAME}: What can I do for you?
+ Shop
    -> ChooseShop
+ Give gift
    -> ChooseGift
+ Leave
    -> EndGeneric

=== ChooseShop ===
-> OpenInterface("ChooseShop", -> EndShop, -> Continue)

=== ChooseGift ===
-> OpenInterface("ChooseGift", -> EndGift, -> PositiveGift)

=== OpenInterface(ChooseEvent, -> EndKnot, -> AdditionalKnot)  ===
>>> TextboxExit();
>>> InvokeUnityEvent({ChooseEvent});
>>> WaitUntilEvent(CloseComplete);
>>> UpdateInkVar(IS_RETURNING, isReturning);
>>> UpdateInkVar(IS_CORRECT_GIFT, isCorrectGift);
>>> TextboxEnter(Default);
-> AdditionalKnot ->
-> Returning(EndKnot)

=== Returning(-> EndKnot)  ===
{ IS_RETURNING:
- 0:
    -> EndKnot
- 1:
    -> InterfaceChoices
}

// Not checking gift anymore since there is only dialogue response for positive
// === CheckGift ===
// >>> UpdateInkVar(IS_CORRECT_GIFT, isCorrectGift);
// { IS_CORRECT_GIFT:
// - 0:
//     {CHARACTER_NAME}: This isn't what I wanted.
// - 1:
//     {CHARACTER_NAME}: I really wanted this! Thanks!
//     {CHARACTER_NAME}: Please take this baked chicken recipe as a reward.
// }
// ->EndFunction

=== Continue ===
->->

=== EndFunction ===
>>> !CharExit({CHARACTER_NAME});
-> END