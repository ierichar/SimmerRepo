VAR IS_RETURNING = 0
VAR IS_CORRECT_GIFT = 1

VAR CHARACTER_NAME = "Bob"

>>> !CharEnter({CHARACTER_NAME})
>>> TextboxEnter(Default)

{CHARACTER_NAME}: Hi! I'm {CHARACTER_NAME}.
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
{CHARACTER_NAME}: Sure thing!
-> OpenInterface("ChooseShop", -> EndShop, -> Continue)

=== ChooseGift ===
{CHARACTER_NAME}: What a surprise!
-> OpenInterface("ChooseGift", -> EndGift, -> CheckGift)

=== OpenInterface(ChooseEvent, -> EndKnot, -> AdditionalKnot)  ===
>>> TextboxExit();
>>> InvokeUnityEvent({ChooseEvent});
>>> WaitUntilEvent(CloseComplete);
>>> UpdateInkVar(IS_RETURNING, isReturning);
>>> TextboxEnter(Default);
-> AdditionalKnot ->
-> Returning(EndKnot)

=== Returning(-> EndKnot)  ===
{ IS_RETURNING:
- 0:
    -> EndKnot
- 1:
    ->InterfaceChoices
}

=== CheckGift ===
>>> UpdateInkVar(IS_CORRECT_GIFT, isCorrectGift);
{ IS_CORRECT_GIFT:
- 0:
    Bob: This isn't what I wanted.
- 1:
    Bob: I really wanted this! Thanks!
    Bob: Please take this baked chicken recipe as a reward.
}
->EndFunction

=== Continue ===
->->

=== EndShop ===
{CHARACTER_NAME}: Pleasure doing business!
-> EndFunction

=== EndGift ===
{CHARACTER_NAME}: Thanks!
-> EndFunction

=== EndGeneric ===
{CHARACTER_NAME}: Have a nice day!
-> EndFunction

=== EndFunction ===
>>> !CharExit({CHARACTER_NAME});
-> END