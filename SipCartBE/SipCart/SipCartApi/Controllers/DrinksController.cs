using Microsoft.AspNetCore.Mvc;
using SipCartCore.Entities;
using SipCartCore.Exceptions;
using SipCartCore.Services.Interfaces;

namespace SipCartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrinksController(ILogger<DrinksController> logger, IDrinkService drinkService) : ControllerBase
    {
        private readonly IDrinkService _drinkService = drinkService;
        private readonly ILogger<DrinksController> _logger = logger;

        [HttpGet("all", Name = "GetAllDrinks")]
        public async Task<ActionResult<IEnumerable<Drink>>> GetAllDrinks()
        {
            try
            {
                IEnumerable<Drink> drinks = await _drinkService.GetAllDrinksAsync();
                return Ok(drinks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("drink/{drinkId}", Name = "GetDrinkById")]
        public async Task<ActionResult<Drink>> GetDrinkById(int drinkId)
        {
            try
            {
                return Ok(await _drinkService.GetDrinkByIdAsync(drinkId));

            }
            catch (Exception ex)
            {
                if (ex is ItemNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return BadRequest();
            }
        }

        [HttpPost("drinks", Name = "GetMultipleDrinksById")]
        public async Task<ActionResult<IEnumerable<Drink>>> GetMultipleDrinksById([FromBody] List<int> ids)
        {
            try
            {
                return Ok(await _drinkService.GetMultipleDrinksByIdAsync(ids));
            }
            catch (Exception ex)
            {
                if (ex is ItemNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return BadRequest();
            }
        }
    }
}
