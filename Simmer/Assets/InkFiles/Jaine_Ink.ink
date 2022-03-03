VAR IS_RETURNING = 0

VAR CHARACTER_NAME = "Jaine"

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
-> OpenInterface("ChooseShop", -> EndShop)

=== ChooseGift ===
{CHARACTER_NAME}: What a surprise!
-> OpenInterface("ChooseGift", -> EndGift)

=== OpenInterface(ChooseEvent, -> EndKnot)  ===
>>> TextboxExit();
>>> InvokeUnityEvent({ChooseEvent});
>>> WaitUntilEvent(CloseComplete);
>>> UpdateInkVar(IS_RETURNING, isReturning);
>>> TextboxEnter(Default);
-> Returning(EndKnot)

=== Returning(-> EndKnot)  ===
{ IS_RETURNING:
- 0:
    -> EndKnot
- 1:
    ->InterfaceChoices
}

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