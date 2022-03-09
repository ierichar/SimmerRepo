VAR CHARACTER_NAME = "Tutorial"

>>> !CharEnter({CHARACTER_NAME});
>>> TextboxEnter(Default)
// [Playable character sprite]
{CHARACTER_NAME}: Welcome to Simmer!
{CHARACTER_NAME}:Your goal is to learn and make as many dishes as you can.
{CHARACTER_NAME}: Before you jump in, here are some things you should know

// Farmer’s Market
>>> InvokeUnityEvent(NextSlide);
// [Shopkeeper sprite] 
{CHARACTER_NAME}: Shopkeepers provide quests for new recipes as well as ingredients to use.
{CHARACTER_NAME}: Talk to the shopkeepers at the farmer’s market to receive new information about recipes

>>> InvokeUnityEvent(NextSlide);
// [Recipe Book Icon] comes into frame
{CHARACTER_NAME}: To check recipes you’ve learned, access the Recipe Book
>>> InvokeUnityEvent(NextSlide);
// [Screenshot of full game screen including “egg ingredient going into recipe book upon discovery”] 
{CHARACTER_NAME}: Items you find will be added to your recipe book…

>>> InvokeUnityEvent(NextSlide);
// [Image of egg ingredient now visually shown in the recipe book]
{CHARACTER_NAME}: There, you can see how to use the ingredients you’ve unlocked.
>>> InvokeUnityEvent(NextSlide);

// [Image of example utility map for egg includes: egg, pasta dough, cake batter, cake base and cream cake]
{CHARACTER_NAME}: The utility map allows you to see how an ingredient is included in recipes.

>>> InvokeUnityEvent(NextSlide);
// [Image of an example Recipe Map from the cream cake]
{CHARACTER_NAME}:The recipe map allows you to see all the components of a recipe as well as the ingredients you have unlocked.

>>> InvokeUnityEvent(NextSlide);
// [Stove Icon] comes into frame
{CHARACTER_NAME}: Use the appliances in your kitchen to make exciting recipes!
{CHARACTER_NAME}: You can still make a dish even without knowing all of the ingredients..

{CHARACTER_NAME}: Have fun cooking!

>>> !CharExit({CHARACTER_NAME});