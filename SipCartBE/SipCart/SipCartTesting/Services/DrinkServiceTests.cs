using SipCartCore.Entities;
using SipCartCore.Exceptions;
using SipCartCore.Services.Interfaces;
using SipCartTesting.Setup;

namespace SipCartCore.Services.Tests
{
    [TestFixture()]
    public class DrinkServiceTests : InMemoryTestSuite
    {

        private IDrinkService _sut;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            await GetContextAsync();
            if (_appContext.Drinks.Count() == 0)
            {
                _appContext.Drinks.Add(new Drink { Id = 1, Name = "Test Drink 1", Price = 1.99m });
                _appContext.Drinks.Add(new Drink { Id = 2, Name = "Test Drink 2", Price = 2.99m });
                _appContext.Drinks.Add(new Drink { Id = 3, Name = "Test Drink 3", Price = 3.99m });
                await _appContext.SaveChangesAsync();
            }
            _sut = new DrinkService(_appContext);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await CleanUpAsync();
        }


        [Test(Description =
            "WHEN get all" +
            "THEN get all 3 items")]
        [Order(1)]
        public async Task GetAllDrinksAsyncTest()
        {
            IEnumerable<Drink> res = await _sut.GetAllDrinksAsync();
            Assert.That(res.Count(), Is.EqualTo(3));
        }

        [Test(Description =
            "WHEN get multiple drinks by 2 ids" +
            "THEN get 2 items"
            )]
        [Order(2)]
        public async Task GetMultipleDrinksByIdAsyncTest()
        {
            int[] ids = new[] { 1, 2 };
            IEnumerable<Drink> res = await _sut.GetMultipleDrinksByIdAsync(ids);
            Assert.That(res.Count(), Is.EqualTo(2));
        }

        [Test(Description =
            "WHEN get a item by id" +
            "THEN get the item"
            )]
        [Order(3)]
        public async Task GetDrinkByIdAsyncTest()
        {
            int id = 1;
            Drink res = await _sut.GetDrinkByIdAsync(id);
            Assert.That(res.Id, Is.EqualTo(id));
        }


        [Test(Description =
            "WHEN get a item by invalid id" +
            "THEN get the exception"
            )]
        [Order(4)]
        public async Task GetDrinkByIdAsyncTest_ThrowsItemNotFoundException()
        {
            int id = 4;
            Assert.ThrowsAsync<ItemNotFoundException>(async () => await _sut.GetDrinkByIdAsync(id));
        }
    }
}