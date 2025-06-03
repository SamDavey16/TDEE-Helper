using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using WeightTracker.Helpers;
using WeightTracker.Interfaces;
using WeightTracker.Services;
using WeightTracker.Services.ActivityLevelStrategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddTransient<Sedentry>();
builder.Services.AddTransient<Moderate>();
builder.Services.AddTransient<VeryActive>();
builder.Services.AddSingleton<ActivityStrategyResolver>();

builder.Services.AddTransient<MifflinStJeorFormula>();
builder.Services.AddTransient<HarrisBenedictFormula>();
builder.Services.AddSingleton<TDEEFormulaResolver>();

builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(); 
app.UseAuthorization();
app.MapControllers();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "clientapp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
