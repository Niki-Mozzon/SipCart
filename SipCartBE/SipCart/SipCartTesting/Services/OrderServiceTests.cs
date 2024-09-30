using Moq;
using SipCartCore.Entities;
using SipCartCore.Services.Interfaces;
using SipCartTesting.Setup;

namespace SipCartCore.Services.Tests
{
    [TestFixture()]
    public class OrderServiceTests : InMemoryTestSuite
    {
        private IOrderService _sut;
        private Mock<ICouponService> _couponServiceMock;
        private Mock<IDrinkService> _drinkServiceMock;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            _couponServiceMock = new Mock<ICouponService>();
            _drinkServiceMock = new Mock<IDrinkService>();
            await GetContextAsync();
            _sut = new OrderService(_appContext
                , _couponServiceMock.Object
                , _drinkServiceMock.Object
                );
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await CleanUpAsync();
        }

        [Test(Description =
            "GIVEN order details" +
            "WHEN purchased" +
            "THEN return order id"
            )]
        [Order(1)]
        public async Task AddOrderAsyncTest()
        {
            int? id = await _sut.AddOrderAsync(12, "abc", ePaymentMethod.CARD);
            Assert.That(id, Is.Not.Null);
            Assert.That(id, Is.Not.EqualTo(0));
        }

        [Test(Description =
            "GIVEN order detail with price above 10 and Cash as payment method" +
            "WHEN purchased" +
            "THEN throw exception"
            )]
        [Order(2)]
        public async Task AddOrderAsyncTest_ThrowException()
        {
            Assert.ThrowsAsync<Exception>(async () => await _sut.AddOrderAsync(12, "abc", ePaymentMethod.CASH));
        }

        [Test(Description =
            "WHEN get all orders" +
            "THEN return all orders"
            )]
        [Order(3)]
        public async Task GetAllOrdersAsyncTest()
        {
            IEnumerable<Order> res = await _sut.GetAllOrdersAsync();
            Assert.That(res.Count(), Is.EqualTo(1));
        }

        [Test(Description =
            "GIVEN a cart with 3 items" +
            "WHEN check out and create order" +
            "THEN return correct order details"
            )]
        [Order(4)]
        public async Task CheckOutAndCreateOrderTestAsync()
        {
            // Arrange
            Dictionary<int, int> items = new Dictionary<int, int> { { 1, 2 }, { 2, 1 } }; // 2 Cokes and 1 Pepsi
            string couponCode = "DISCOUNT10";
            Coupon coupon = new Coupon { Code = couponCode, PercentageReduction = 50 };
            decimal expectedTotalPrice = 4m;
            decimal expectedFullPrice = 8m;

            // Mock
            List<Drink> drinks = new List<Drink>
            {
                new Drink { Id = 1, Name = "Coke", Price = 2.5m },
                new Drink { Id = 2, Name = "Pepsi", Price = 3.0m }
            };
            _drinkServiceMock.Setup(ds => ds.GetMultipleDrinksByIdAsync(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(drinks);
            _couponServiceMock.Setup(cs => cs.GetCouponByCodeAsync(It.IsAny<string>()))
                .ReturnsAsync(coupon);

            // Act
            OrderDetail result = await _sut.CheckOutAndCreateOrderAsync(items, couponCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Cart.Count);
            Assert.AreEqual(couponCode, result.CouponCode);
            Assert.AreEqual(expectedFullPrice, result.FullPrice);
            Assert.AreEqual(expectedTotalPrice, result.TotalPrice);

        }

    }
}