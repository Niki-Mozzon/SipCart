using SipCartCore.Entities;

namespace SipCartCore.Services.Interfaces
{
    public interface IDrinkService
    {
        Task<IEnumerable<Drink>> GetAllDrinksAsync();
        Task<Drink> GetDrinkByIdAsync(int drinkId);
        Task<IEnumerable<Drink>> GetMultipleDrinksByIdAsync(IEnumerable<int> ids);
    }
}