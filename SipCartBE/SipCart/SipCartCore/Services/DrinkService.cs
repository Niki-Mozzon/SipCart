using Microsoft.EntityFrameworkCore;
using SipCartCore.Entities;
using SipCartCore.Exceptions;
using SipCartCore.Services.Interfaces;

namespace SipCartCore.Services
{
    public class DrinkService(AppContext context) : IDrinkService
    {
        private readonly AppContext _context = context;

        public async Task<IEnumerable<Drink>> GetAllDrinksAsync()
        {
            List<Drink> drinks = await _context.Drinks.ToListAsync();
            return drinks;
        }

        public async Task<IEnumerable<Drink>> GetMultipleDrinksByIdAsync(IEnumerable<int> ids)
        {
            List<Drink> drinks = await _context.Drinks.Where(order => ids.Contains(order.Id)).ToListAsync();
            if (drinks.Count == 0)
            {
                throw new ItemNotFoundException("No drinks found with the provided ids");
            }

            return drinks;
        }

        public async Task<Drink> GetDrinkByIdAsync(int drinkId)
        {
            Drink? drink = await _context.Drinks.FirstOrDefaultAsync(order => order.Id == drinkId);
            if (drink == null)
            {
                throw new ItemNotFoundException(drinkId);
            }
            return drink;
        }
    }
}
