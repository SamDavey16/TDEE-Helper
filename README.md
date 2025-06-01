# TDEE-Helper
A tool which lets you choose a TDEE formula to calculate your Total Daily Energy Expenditure to help you manage weight gain/loss.

The application utilises the strategy pattern to calculate your TDEE based off of which formula you choose.
The IFormulaStrategy interface defines the Basic Metabolic Rate calculation method as well as the name identification which will be needed for either formula as both require the same parameters, this provides the advantage of being able to add additional formulas in the future without having to change the core logic of the application allowing for it to scale well, directly adhering to the Open/Closed principle.

I used dependency injection on this project to be able to implement different strategies in a clean way rather than having to build out the dependencies around the codebase, the same benefit the strategy pattern provides.
Each strategy is injected as a transient service as each calculation is only required once and is likely to be different when it is next called. The resolvers are registered as singletons because they are stateless and only return strategy instances which are registered as transient. This ensures that each time a strategy is resolved a new instance is used for accurate and isolated calculations. Registering the resolver as a singleton reduces overhead by creating only one instance of the resolver for the application's lifetime, improving memory efficiency without affecting correctness.

The database runs in a linux container and entity framework migrations are used to keep the database schema up to date. The database helper class is used for all database functions across the application.
