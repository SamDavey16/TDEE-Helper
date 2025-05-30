using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeightTracker.Helpers;
using WeightTracker.Services;
using WeightTracker.Services.ActivityLevelStrategies;

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<Context>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("Default")));

                services.AddTransient<Sedentry>();
                services.AddTransient<Moderate>();
                services.AddTransient<VeryActive>();
                services.AddSingleton<ActivityStrategyResolver>();

                services.AddTransient<MifflinStJeorFormula>();
                services.AddTransient<HarrisBenedictFormula>();
                services.AddSingleton<TDEEFormulaResolver>();
            })
            .Build();

using var scope = host.Services.CreateScope();
var provider = scope.ServiceProvider;

Console.WriteLine("Hi! If you are an existing user, please enter your UserId to retrieve your current TDEE. If you are a new user, you can skip this step.");
var idEntry = Console.ReadLine();

if (!string.IsNullOrEmpty(idEntry) && int.TryParse(idEntry, out int userId))
{
    try
    {
        await DatabaseHelper.GetCurrentTDEE(userId);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving current TDEE: {ex.Message}");
        throw;
    }
}
else
{
    Console.WriteLine("No UserId provided or invalid input. Proceeding to TDEE calculation. Enter Name: ");
    string name = Console.ReadLine()!;
    userId = await DatabaseHelper.CreateNewUser(new WeightTracker.Models.Users
    {
        Name = name
    });
}

Console.Write("Enter your weight (kg): ");
double weight = double.Parse(Console.ReadLine()!);

Console.Write("Enter your height (cm): ");
double height = double.Parse(Console.ReadLine()!);

Console.Write("Enter your age (years): ");
int age = int.Parse(Console.ReadLine()!);

Console.Write("Enter your sex (M/F): ");
string sex = Console.ReadLine()!;

Console.WriteLine("\nChoose TDEE Formula:\n 1. Mifflin-St Jeor (Recommended)\n 2. Harris-Benedict");
var formulaChoice = Console.ReadLine()!;
var formula = provider.GetRequiredService<TDEEFormulaResolver>().Resolve(formulaChoice);

Console.WriteLine("\nChoose activity level:\n 1. Sedentary\n 2. Moderate\n 3. Very Active");
var activityChoice = Console.ReadLine()!;
var activity = provider.GetRequiredService<ActivityStrategyResolver>().Resolve(activityChoice);

var calculator = new TDEECalculator(formula, activity);
var tdee = calculator.CalculateTDEE(weight, height, age, sex);

try
{
    await DatabaseHelper.AddEntry(new WeightTracker.Models.Entries
    {
        UserId = userId,
        Weight = (decimal)weight,
        TDEE = (int)tdee
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Error saving entry: {ex.Message}");
}

Console.WriteLine($"\nUsing formula: {formula.Name}");
Console.WriteLine($"Your estimated TDEE is: {tdee:F2} kcal/day");