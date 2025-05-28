# TDEE-Helper
A tool which lets you choose a TDEE formula to calculate your Total Daily Energy Expenditure to help you manage weight gain/loss.

The application utilises the strategy pattern to calculate your TDEE based off of which formula you choose.
The IFormulaStrategy interface defines the Basic Metabolic Rate calculation method as well as the name identification which will be needed for either formula as both require the same parameters, this provides the advantage of being able to add additional formulas in the future without having to change the core logic of the application allowing for it to scale well, directly adhering to the Open/Closed principle.
