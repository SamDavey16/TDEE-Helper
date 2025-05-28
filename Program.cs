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

Console.WriteLine($"\nUsing formula: {formula.Name}");
Console.WriteLine($"Your estimated TDEE is: {tdee:F2} kcal/day");