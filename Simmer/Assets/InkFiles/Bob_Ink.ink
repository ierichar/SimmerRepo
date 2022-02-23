VAR IS_RETURNING = 0

>>> !CharEnter(Bob)
>>> TextboxEnter(Default)

Bob: Hi! I'm Bob.
-> InterfaceChoices

=== InterfaceChoices ===
Bob: What can I do for you?
+ Shop
    -> ChooseShop
+ Give gift
    -> ChooseGift
+ Leave
    -> EndGeneric

=== ChooseShop ===
Bob: Sure thing!
-> OpenInterface("ChooseShop", -> EndShop)

=== ChooseGift ===
Bob: What a surprise!
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
Bob: Pleasure doing business!
-> EndFunction

=== EndGift ===
Bob: Thanks!
-> EndFunction

=== EndGeneric ===
Bob: Have a nice day!
-> EndFunction

=== EndFunction ===
>>> !CharExit(Bob);
-> END