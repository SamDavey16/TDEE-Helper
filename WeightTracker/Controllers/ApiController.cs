using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeightTracker.Helpers;
using WeightTracker.Interfaces;
using WeightTracker.Models;
using WeightTracker.Services;

[ApiController]
[Route("api/[controller]")]
public class TDEEController : ControllerBase
{
    private readonly IDatabaseHelper _dbHelper;
    private readonly TDEEFormulaResolver _formulaResolver;
    private readonly ActivityStrategyResolver _activityResolver;

    public TDEEController(IDatabaseHelper dbHelper, TDEEFormulaResolver formulaResolver, ActivityStrategyResolver activityResolver)
    {
        _dbHelper = dbHelper;
        _formulaResolver = formulaResolver;
        _activityResolver = activityResolver;
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] Users userDto)
    {
        var user = new Users { Name = userDto.Name };
        var userId = await _dbHelper.CreateNewUser(user);
        return Ok(new { UserId = userId });
    }

    [HttpPost("tdee/calculate")]
    public async Task<IActionResult> CalculateTDEE([FromBody] Entries dto)
    {
        var formula = _formulaResolver.Resolve(dto.FormulaChoice);
        var activity = _activityResolver.Resolve(dto.ActivityChoice);

        var calculator = new TDEECalculator(formula, activity);
        int tdee = Convert.ToInt32(calculator.CalculateTDEE(dto.Weight, dto.Height, dto.Age, dto.Sex));
        dto.TDEE = tdee;

        await _dbHelper.AddEntry(dto);

        return Ok(new { TDEE = tdee });
    }

    [HttpGet("users/{userId}/tdee")]
    public async Task<IActionResult> GetTDEE(int userId)
    {
        try
        {
            int tdee = await _dbHelper.GetCurrentTDEE(userId);
            return Ok(new { TDEE = tdee });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

